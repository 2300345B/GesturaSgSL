from flask import Flask, jsonify
from camera_stream import GestureDetector
import threading

app = Flask(__name__)

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

if __name__ == '__main__':
    print("🚀 Starting Flask server and camera stream...")
    app.run(host='0.0.0.0', port=5000)
