using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PanelTransition : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject gamePanel;
    public float fadeDuration = 1f;

    private CanvasGroup mainGroup;
    private CanvasGroup gameGroup;

    void Start()
    {
        mainGroup = mainPanel.GetComponent<CanvasGroup>();
        if (mainGroup == null) mainGroup = mainPanel.AddComponent<CanvasGroup>();

        gameGroup = gamePanel.GetComponent<CanvasGroup>();
        if (gameGroup == null) gameGroup = gamePanel.AddComponent<CanvasGroup>();

        mainGroup.alpha = 1f;
        gameGroup.alpha = 0f;
        gamePanel.SetActive(false);
    }

    public void Transition()
    {
        StartCoroutine(FadeTransition());
    }

    IEnumerator FadeTransition()
    {
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float a = 1 - t / fadeDuration;
            mainGroup.alpha = a;
            yield return null;
        }

        mainPanel.SetActive(false);
        gamePanel.SetActive(true);

        t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float a = t / fadeDuration;
            gameGroup.alpha = a;
            yield return null;
        }

        gameGroup.alpha = 1f;
    }
}
