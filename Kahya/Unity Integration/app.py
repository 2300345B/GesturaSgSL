# app.py
from flask import Flask, jsonify
import camera_stream

app = Flask(__name__)

@app.route('/gesture', methods=['GET'])
def get_gesture():
    return jsonify({
        'gesture': camera_stream.current_gesture,
        'confidence': round(camera_stream.current_confidence, 2)
    })

if __name__ == '__main__':
    app.run(host='0.0.0.0', port=5000)
