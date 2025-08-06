import torch
import cv2
import threading
import mediapipe as mp


class GestureDetector:
    def __init__(self, model_path, camera_index=0):
        # Load YOLOv5 model
        self.model = torch.hub.load('ultralytics/yolov5', 'custom', path=model_path, force_reload=True)
        self.model.eval()

        # Setup MediaPipe hands
        self.mp_hands = mp.solutions.hands
        self.hands = self.mp_hands.Hands(
            static_image_mode=False,
            max_num_hands=1,
            min_detection_confidence=0.5,
            min_tracking_confidence=0.5
        )

        # Open webcam
        self.cap = cv2.VideoCapture(camera_index)
        if not self.cap.isOpened():
            raise Exception(f"❌ Cannot open webcam at index {camera_index}.")
        
        self.cap.set(cv2.CAP_PROP_FRAME_WIDTH, 640)
        self.cap.set(cv2.CAP_PROP_FRAME_HEIGHT, 480)


        # Shared state variables
        self.current_gesture = 'None'
        self.current_confidence = 0.0
        self._running = False
        self._thread = None

    def _detect_loop(self):
        while self._running:
            ret, frame = self.cap.read()
            if not ret:
                continue

            frame = cv2.flip(frame, 1)  # Mirror the frame for natural interaction
            rgb = cv2.cvtColor(frame, cv2.COLOR_BGR2RGB)
            results = self.hands.process(rgb)

            if results.multi_hand_landmarks:
                # Run YOLO model on the frame
                yolo_results = self.model(frame)
                df = yolo_results.pandas().xyxy[0]

                if len(df) > 0:
                    top_result = df.iloc[0]
                    self.current_gesture = top_result['name']
                    self.current_confidence = float(top_result['confidence'])
                else:
                    self.current_gesture = 'Hand Detected - No Match'
                    self.current_confidence = 0.0
            else:
                self.current_gesture = 'No Hand Detected'
                self.current_confidence = 0.0

        # Clean up when stopping
        self.cap.release()

    def start(self):
        if not self._running:
            self._running = True
            self._thread = threading.Thread(target=self._detect_loop, daemon=True)
            self._thread.start()

    def stop(self):
        self._running = False

    def get_gesture(self):
        return self.current_gesture, round(self.current_confidence, 2)
