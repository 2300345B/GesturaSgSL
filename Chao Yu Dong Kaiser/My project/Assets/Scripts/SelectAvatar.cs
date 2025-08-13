
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
//using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

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
        /*if (Camera.main) Debug.Log("");
        if (transform.GetComponentInChildren<Camera>()) Debug.Log("");*/
        Camera.main.transform.position = transform.GetComponentInChildren<Camera>().transform.position;
        Camera.main.transform.rotation = transform.GetComponentInChildren<Camera>().transform.rotation;
        Animator animator = transform.GetComponent<Animator>();
        ModelAnim modelAnim = FindObjectOfType<ModelAnim>();
        modelAnim.animator = animator;
        promptText.text = transform.gameObject.name + " is selected.";
        Outline[] outlines = FindObjectsOfType<Outline>();
        foreach (Outline outline in outlines) outline.enabled = false;
    }
}
/*using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;


public class SelectAvatar : MonoBehaviour
{
    public List<GameObject> avatars;
    public ModelAnim ModelAnim;
    public TMP_Text promptText;

    private void Start()
    {
        // Optional: Auto find the prompt text if you didn't drag it in
        if (promptText == null)
        {
            TMP_Text[] texts = FindObjectsOfType<TMP_Text>(true);
            foreach (TMP_Text text in texts)
            {
                if (text.gameObject.name == "AvatarSelected")
                {
                    promptText = text;
                    break;
                }
            }
        }
    }

    public void AvatarSelected(GameObject avatar)
    {
        {
            Animator animator = avatar.GetComponent<Animator>();
            Debug.Log("Animator found: " + animator);

            if (ModelAnim == null) Debug.Log("ModelAnim is NULL");
            else Debug.Log("ModelAnim is OK");

            ModelAnim.animator = animator;
        }
 
        Camera avatarCamera = avatar.GetComponentInChildren<Camera>();

        if (avatarCamera != null)
        {
            Camera.main.transform.position = avatarCamera.transform.position;
            Camera.main.transform.rotation = avatarCamera.transform.rotation;
        }
        else
        {
            Debug.LogWarning("Avatar does not have a child camera: " + avatar.name);
        }

       
        if (promptText != null)
        {
            promptText.text = avatar.name + " is selected.";
        }

        Outline[] outlines = FindObjectsOfType<Outline>();
        foreach (Outline outline in outlines)
        {
            outline.enabled = false;
        }
    }
}*/