using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GestureDisplay : MonoBehaviour
{
    public Text gestureText;
    public Button startButton;

    private string serverUrl = "http://127.0.0.1:5000";

    void Start()
    {
        startButton.onClick.AddListener(StartDetection);
        StartCoroutine(UpdateGesture());
    }

    void StartDetection()
    {
        StartCoroutine(SendStartRequest());
    }

    IEnumerator SendStartRequest()
    {
        UnityWebRequest request = UnityWebRequest.Post($"{serverUrl}/start", "");
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
            Debug.LogError("Failed to start detector: " + request.error);
    }

    IEnumerator UpdateGesture()
    {
        while (true)
        {
            UnityWebRequest request = UnityWebRequest.Get($"{serverUrl}/gesture");
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                var response = JsonUtility.FromJson<GestureResponse>(request.downloadHandler.text);
                gestureText.text = $"Gesture: {response.gesture}\nConfidence: {response.confidence}";
            }
            else
            {
                gestureText.text = "Error: Cannot connect to Flask server";
            }

            yield return new WaitForSeconds(1f);  // Check every 1 second
        }
    }

    [System.Serializable]
    public class GestureResponse
    {
        public string gesture;
        public float confidence;
    }
}
