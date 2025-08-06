
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
//using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class AvatarSelect : MonoBehaviour
{
    //public List<GameObject> avatars = new List<GameObject>();
    public TMP_Text promptText;
    public Animator animator;
    [SerializeField] private bool _isSelected = false;

    public bool isSelected
    {
        get { return _isSelected; }
        set
        {
            if (value && !_isSelected) // Only run when changing from false to true
            {
                OnSelected();
            }
            else if (!value && _isSelected) // Optional: run when deselected
            {
                OnDeselected();
            }

            _isSelected = value;
        }
    }
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
    void OnSelected()
    {
        Debug.Log("Avatar was selected!");

        // Your code here when selected
        AvatarSelected();
    }

    void OnDeselected()
    {
        Debug.Log("Avatar was deselected!");

        // Your code here when deselected
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AvatarSelected()
    {
        Camera.main.transform.position = transform.GetComponentInChildren<Camera>().transform.position;
        Camera.main.transform.rotation = transform.GetComponentInChildren<Camera>().transform.rotation;
        animator = transform.GetComponent<Animator>();
        GameManager gamManager = FindObjectOfType<GameManager>();
        gamManager.animator = animator;
        gamManager.GetAllTriggers();
        promptText.text = transform.gameObject.name + " is selected.";
        Outline[] outlines = FindObjectsOfType<Outline>();
        foreach (Outline outline in outlines) outline.enabled = false;
    }
}
