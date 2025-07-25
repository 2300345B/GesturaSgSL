using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WebCamVertical : MonoBehaviour
{
    public RawImage rawImage;
    public Toggle webcamToggle;
    private WebCamTexture webCamTexture;

    [Header("Camera Settings")]
    public Vector2 aspectRatio = new Vector2(3, 4);

    private Vector2 lastAspectRatio;
    private RectTransform parentRectTransform;

    void Start()
    {
        webCamTexture = new WebCamTexture();
        rawImage.texture = webCamTexture;
        rawImage.material.mainTexture = webCamTexture;

        parentRectTransform = rawImage.transform.parent.GetComponent<RectTransform>();
        lastAspectRatio = aspectRatio;

        // Initial size adjustment
        UpdateParentSize();

        webcamToggle.onValueChanged.AddListener(OnToggleChanged);

        // Start monitoring for aspect ratio changes
        StartCoroutine(MonitorAspectRatio());
    }

    void Update()
    {
        // Check if aspect ratio changed in inspector or via code
        if (lastAspectRatio != aspectRatio)
        {
            UpdateParentSize();
            lastAspectRatio = aspectRatio;
        }
    }

    IEnumerator MonitorAspectRatio()
    {
        while (true)
        {
            // Wait for webcam to initialize and check if size needs updating
            if (webCamTexture != null && webCamTexture.width > 16 && webCamTexture.height > 16)
            {
                // You can add logic here to automatically adjust based on webcam's actual aspect ratio
                // if needed, or just rely on the manual aspectRatio setting
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    void UpdateParentSize()
    {
        if (parentRectTransform != null)
        {
            parentRectTransform.sizeDelta = AdjustRatio();
        }
    }

    Vector2 AdjustRatio()
    {
        float y = parentRectTransform.sizeDelta.y;
        float x = y * aspectRatio.x / aspectRatio.y;

        return new Vector2(x, y);
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

    // Public method to change aspect ratio at runtime
    public void SetAspectRatio(Vector2 newAspectRatio)
    {
        aspectRatio = newAspectRatio;
        UpdateParentSize();
    }

    // Convenience methods for common aspect ratios
    public void SetAspectRatio(float width, float height)
    {
        SetAspectRatio(new Vector2(width, height));
    }

    // Set to common ratios
    public void SetTo9x16() => SetAspectRatio(9, 16);
    public void SetTo4x3() => SetAspectRatio(4, 3);
    public void SetTo3x4() => SetAspectRatio(3, 4);
    public void SetTo1x1() => SetAspectRatio(1, 1);

    void OnDisable()
    {
        if (webCamTexture != null)
            webCamTexture.Stop();
    }

    void OnValidate()
    {
        // This ensures the size updates when you change aspectRatio in the inspector
        if (Application.isPlaying && parentRectTransform != null)
        {
            UpdateParentSize();
        }
    }
}