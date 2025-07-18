using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SelectAvatar : MonoBehaviour
{
    public List<GameObject> avatars = new List<GameObject>();
    public TMP_Text promptText;
    // Start is called before the first frame update
    void Start()
    {
        TMP_Text[] texts = FindObjectsOfType<TMP_Text>(true);
        foreach (TMP_Text text in texts)
        {
            if (text.gameObject.name == "AvatarSelected")
            {
                promptText = text;
                return;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AvatarSelected()
    {
        Camera.main.transform.position = transform.GetComponentInChildren<Camera>().transform.position;
        Camera.main.transform.rotation = transform.GetComponentInChildren<Camera>().transform.rotation;
        Animator animator = transform.GetComponent<Animator>();
        GoScene goScene = FindObjectOfType<GoScene>();
        goScene.animator = animator;
                promptText.text = transform.gameObject.name + " is selected.";
        Outline[] outlines = FindObjectsOfType<Outline>();
        foreach (Outline outline in outlines) outline.enabled = false;
    }
}
