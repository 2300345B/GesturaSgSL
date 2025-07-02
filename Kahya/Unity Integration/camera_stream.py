import torch
import cv2
import threading

# Load YOLOv5 model
model = torch.hub.load('ultralytics/yolov5', 'custom',
                       path=r"C:\Users\Admin\Documents\GitHub\GesturaSgSL\Kahya\sgsl_yolo\yolov5\runs\train\sgsl_model_improved v3\weights\best.pt",
                       force_reload=True)
model.eval()

# Open webcam
cap = cv2.VideoCapture(0)

if not cap.isOpened():
    print("❌ ERROR: Cannot open webcam. Try changing the index (e.g., to 1 or 2).")
    exit()

# Shared variables
current_gesture = 'None'
current_confidence = 0.0

# Detection and display loop
def detect_loop():
    global current_gesture, current_confidence
    while True:
        ret, frame = cap.read()
        if not ret:
            print("❌ ERROR: Failed to read frame.")
            continue

        # YOLOv5 inference
        results = model(frame)

        # Extract labels and confidence
        df = results.pandas().xyxy[0]

        if len(df) > 0:
            top_result = df.iloc[0]
            current_gesture = top_result['name']
            current_confidence = float(top_result['confidence'])
        else:
            current_gesture = 'None'
            current_confidence = 0.0

        print(f"Gesture: {current_gesture}, Confidence: {current_confidence:.2f}")

        # Draw bounding boxes on the image
        annotated_frame = results.render()[0]  # returns list of ndarray

        # Display the image
        cv2.imshow("Gesture Detection", annotated_frame)

        # Press Q to quit (in debug)
        if cv2.waitKey(1) & 0xFF == ord('q'):
            break

    cap.release()
    cv2.destroyAllWindows()

# Start thread
t = threading.Thread(target=detect_loop)
t.daemon = True
t.start()
