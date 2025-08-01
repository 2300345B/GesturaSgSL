using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GestureDisplay : MonoBehaviour
{
    public Text gestureText;
    public Button aiButton;

    private string serverUrl = "http://127.0.0.1:5000";
    private bool isDetecting = false;

    void Start()
    {
        aiButton.onClick.AddListener(StartGestureDetection);
    }

    void StartGestureDetection()
    {
        if (!isDetecting)
        {
            isDetecting = true;
            StartCoroutine(SendStartRequest());
            StartCoroutine(UpdateGesture());
        }
    }

    IEnumerator SendStartRequest()
    {
        UnityWebRequest request = UnityWebRequest.Post($"{serverUrl}/start", "");
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            gestureText.text = "⚠️ Failed to start gesture detector.";
            Debug.LogError("Failed to start detector: " + request.error);
        }
        else
        {
            gestureText.text = "🟢 Camera started. Waiting for gesture...";
        }
    }

    IEnumerator UpdateGesture()
    {
        while (isDetecting)
        {
            UnityWebRequest request = UnityWebRequest.Get($"{serverUrl}/gesture");
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                var response = JsonUtility.FromJson<GestureResponse>(request.downloadHandler.text);
                gestureText.text = $"✅ Great! I can recognize it is: <b>{response.gesture}</b>\nConfidence: {response.confidence:F2}";
            }
            else
            {
                gestureText.text = "❌ Error: Cannot connect to Flask server.";
            }

            yield return new WaitForSeconds(1f); // Poll every second
        }
    }

    [System.Serializable]
    public class GestureResponse
    {
        public string gesture;
        public float confidence;
    }
}