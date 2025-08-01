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
        WWWForm form = new WWWForm();
        UnityWebRequest request = UnityWebRequest.Post($"{serverUrl}/start", form);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            gestureText.text = "⚠️ Failed to start gesture detector.";
            Debug.LogError("❌ POST /start failed: " + request.error);
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
                GestureResponse response = JsonUtility.FromJson<GestureResponse>(request.downloadHandler.text);
                gestureText.text = $"✅ Recognized: <b>{response.gesture}</b>\nConfidence: {response.confidence:F2}";
            }
            else
            {
                gestureText.text = "❌ Error: Cannot connect to Flask server.";
                Debug.LogError("❌ GET /gesture failed: " + request.error);
            }

            yield return new WaitForSeconds(1f);
        }
    }

    [System.Serializable]
    public class GestureResponse
    {
        public string gesture;
        public float confidence;
    }
}
