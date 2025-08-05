using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CameraStreamDisplay : MonoBehaviour
{
    public RawImage displayImage;
    public string streamUrl = "http://127.0.0.1:5000/video_feed";

    void Start()
    {
        StartCoroutine(LoadStream());
    }

    IEnumerator LoadStream()
    {
        while (true)
        {
            UnityWebRequest req = UnityWebRequestTexture.GetTexture(streamUrl);
            yield return req.SendWebRequest();

            if (req.result == UnityWebRequest.Result.Success)
            {
                Texture2D tex = ((DownloadHandlerTexture)req.downloadHandler).texture;
                displayImage.texture = tex;
            }

            yield return new WaitForSeconds(0.1f); // Avoid spamming server
        }
    }
}
