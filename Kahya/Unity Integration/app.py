from flask import Flask, jsonify, Response
from camera_stream import GestureDetector
import cv2

app = Flask(__name__)

# Path to your trained YOLOv5 model
MODEL_PATH = r'C:\Users\Admin\Documents\GitHub\GesturaSgSL\Kahya\sgsl_yolo\AI Models\sgsl_model_v3\weights\best.pt'
gesture_detector = GestureDetector(model_path=MODEL_PATH)

@app.route('/start', methods=['POST'])
def start_detector():
    gesture_detector.start()
    return jsonify({"status": "started"})

@app.route('/gesture', methods=['GET'])
def get_gesture():
    gesture, confidence = gesture_detector.get_gesture()
    return jsonify({
        'gesture': gesture,
        'confidence': confidence
    })

@app.route('/video_feed')
def video_feed():
    def generate_frames():
        while True:
            if not gesture_detector.cap.isOpened():
                continue
            ret, frame = gesture_detector.cap.read()
            if not ret:
                continue
            frame = cv2.flip(frame, 1)
            _, buffer = cv2.imencode('.jpg', frame)
            frame_bytes = buffer.tobytes()
            yield (b'--frame\r\n'
                   b'Content-Type: image/jpeg\r\n\r\n' + frame_bytes + b'\r\n')

    return Response(generate_frames(), mimetype='multipart/x-mixed-replace; boundary=frame')

if __name__ == '__main__':
    print("🚀 Starting Flask server and clean video stream...")
    app.run(host='0.0.0.0', port=5000)
        
