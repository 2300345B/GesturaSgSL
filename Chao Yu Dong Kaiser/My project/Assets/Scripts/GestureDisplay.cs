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

    // Gesture practice settings
    public string[] gestureSequence = { "I love you", "Yes", "No", "Me", "You" };
    private int currentGestureIndex = 0;
    private float confidenceThreshold = 0.6f;

    void Start()
    {
        aiButton.onClick.AddListener(StartGestureDetection);
    }

    void StartGestureDetection()
    {
        if (!isDetecting)
        {
            isDetecting = true;
            currentGestureIndex = 0;
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
            gestureText.text = $"👋 Let's begin! Show me: {gestureSequence[currentGestureIndex]}";
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
                string targetGesture = gestureSequence[currentGestureIndex];
                float accuracyPercent = Mathf.Round(response.confidence * 100f);

                if (response.gesture.Equals(targetGesture, System.StringComparison.OrdinalIgnoreCase) &&
                    response.confidence >= confidenceThreshold)
                {
                    // Correct gesture — move to next
                    currentGestureIndex++;

                    if (currentGestureIndex >= gestureSequence.Length)
                    {
                        gestureText.text = "🎉 Fantastic! You've mastered all the gestures!";
                        isDetecting = false;
                        yield break;
                    }
                    else
                    {
                        gestureText.text = $"✅ Great job! You nailed '{response.gesture}' ({accuracyPercent}%).\nNext: {gestureSequence[currentGestureIndex]}";
                    }
                }
                else
                {
                    // Encouraging prompt
                    gestureText.text = $"✨ Try showing: {targetGesture}\nI think I see '{response.gesture}' ({accuracyPercent}%)";
                }
            }
            else
            {
                gestureText.text = "❌ Error: Cannot connect to AI assistant.";
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
