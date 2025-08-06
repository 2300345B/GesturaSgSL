using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class CameraStreamDisplay : MonoBehaviour
{
    public RawImage displayImage;
    public string frameUrl = "http://127.0.0.1:5000/frame.jpg";

    private Coroutine fetchRoutine;

    // Called when GameObject is enabled (e.g. panel opens)
    void OnEnable()
    {
        fetchRoutine = StartCoroutine(FetchFrameLoop());
    }

    // Called when GameObject is disabled (e.g. panel closed)
    void OnDisable()
    {
        if (fetchRoutine != null)
        {
            StopCoroutine(fetchRoutine);
        }
    }

    IEnumerator FetchFrameLoop()
    {
        while (true)
        {
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(frameUrl);
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Texture tex = ((DownloadHandlerTexture)request.downloadHandler).texture;
                displayImage.texture = tex;
            }
            else
            {
                Debug.LogWarning("⚠️ Failed to fetch frame: " + request.error);
            }

            yield return new WaitForSeconds(0.016f); 
        }
    }
}
