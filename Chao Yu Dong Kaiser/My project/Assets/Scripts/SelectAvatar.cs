using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;

public class SelectAvatar : MonoBehaviour
{
    public List<GameObject> avatars = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        Animator[] animators = FindObjectsOfType<Animator>();
        foreach (Animator animator in animators) avatars.Add(animator.gameObject);
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
    }
}
