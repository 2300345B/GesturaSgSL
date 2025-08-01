using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GestureDisplay : MonoBehaviour
{
    public Text gestureText;
    public Button startButton;
    public GameObject gestureManager;  // Drag the GestureManager GameObject here
    public GameObject cameraManager;  // Drag the CameraManager GameObject here



    private string serverUrl = "http://127.0.0.1:5000";
    private bool detectionStarted = false;

    void Start()
    {
        startButton.onClick.AddListener(OnButtonClick);
    }

    public void OnButtonClick()
{
    if (!detectionStarted)
    {
        detectionStarted = true;

        if (gestureManager != null)
            gestureManager.SetActive(true);  // Show GestureManager (and its children)

        StartCoroutine(UpdateGesture());
    }
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
                gestureText.text = $"Great! I can recognize it is {response.gesture}\nConfidence: {response.confidence}";
            }
            else
            {
                gestureText.text = "Error: Cannot connect to Flask server";
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
