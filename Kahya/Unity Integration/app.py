from flask import Flask, jsonify, Response
from camera_stream import GestureDetector
import cv2

app = Flask(__name__)

# Load your YOLOv5 model
MODEL_PATH = r"C:\Users\kaise\Downloads\GesturaSgSL\Kahya\sgsl_yolo\AI Models\sgsl_model_v2\weights\best.pt" #change file name accordingly
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

# ✅ This endpoint returns one clean frame for Unity's RawImage
@app.route('/frame.jpg')
def single_frame():
    if not gesture_detector.cap.isOpened():
        return "Camera not available", 500

    ret, frame = gesture_detector.cap.read()
    if not ret:
        return "Frame read failed", 500

    frame = cv2.flip(frame, 1)  # Mirror for natural view
    _, buffer = cv2.imencode('.jpg', frame)
    return Response(buffer.tobytes(), mimetype='image/jpeg')

if __name__ == '__main__':
    print("🚀 Starting Flask server...")
    app.run(host='0.0.0.0', port=5000)
