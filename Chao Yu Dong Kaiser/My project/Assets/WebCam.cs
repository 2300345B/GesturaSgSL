using UnityEngine;
using UnityEngine.UI;

public class WebCam : MonoBehaviour
{
    public RawImage rawImage;
    public Toggle webcamToggle;

    private WebCamTexture webCamTexture;

    void Start()
    {
        webCamTexture = new WebCamTexture();
        rawImage.texture = webCamTexture;
        rawImage.material.mainTexture = webCamTexture;

        webcamToggle.onValueChanged.AddListener(OnToggleChanged);
    }

    void OnToggleChanged(bool isOn)
    {
        if (isOn)
        {
            webCamTexture.Play();
            rawImage.enabled = true;
        }
        else
        {
            webCamTexture.Stop();
            rawImage.enabled = false;
        }
    }

    void OnDisable()
    {
        if (webCamTexture != null)
            webCamTexture.Stop();
    }
}
