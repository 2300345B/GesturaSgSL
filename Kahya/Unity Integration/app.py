# app.py

from flask import Flask, jsonify
from camera_stream import GestureDetector
import threading

app = Flask(_name_)

# Update with your actual model path
MODEL_PATH = r'C:\Users\kaise\Documents\GitHub\GesturaSgSL\Kahya\sgsl_yolo\AI Models\sgsl_model_v3\weights\best.pt'

gesture_detector = GestureDetector(model_path=MODEL_PATH)

def start_detector():
    gesture_detector.start()

threading.Thread(target=start_detector, daemon=True).start()

@app.route('/gesture', methods=['GET'])
def get_gesture():
    gesture, confidence = gesture_detector.get_gesture()
    return jsonify({
        'gesture': gesture,
        'confidence': confidence
    })

if _name_ == '_main_':
    print("🚀 Starting Flask server and camera stream...")
    app.run(host='0.0.0.0', port=5000)