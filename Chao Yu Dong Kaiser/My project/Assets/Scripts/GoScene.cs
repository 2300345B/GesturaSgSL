using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoScene : MonoBehaviour
{
    public Animator animator;
    public List<string> triggerNames;
    public List<string> triggerValues;
    public GameObject allToggles;
    public GameObject allAvatars;
    public Toggle[] toggles;
    // Start is called before the first frame update
    void Start()
    {
        SelectAvatarZH[] selectAvatars = FindObjectsOfType<SelectAvatarZH>();
        toggles = allToggles.GetComponentsInChildren<Toggle>();
        for (int i = 0; i < toggles.Length; i++)
        {
            if (toggles[i].isOn)
            {
                selectAvatars[i].isSelected = true;
                return;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void Settings()
    {
        SceneManager.LoadScene("Settings");
    }
    public void BackToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void GoAbout()
    {
        SceneManager.LoadScene("AboutScene");
    }
    public void Repeat()
    {
        
    }

    public void PlayAnimation1()
    {
        animator.SetTrigger("Hi");
    }
    public void PlayAnimation2()
    {
        animator.SetTrigger("HowAreYou");

    }

    public void GetAllTriggers()
    {
        if (animator == null || animator.runtimeAnimatorController == null)
            return;

        // Get all parameters from the controller
        AnimatorControllerParameter[] parameters = animator.parameters;

        Debug.Log("All Triggers:");

        foreach (AnimatorControllerParameter param in parameters)
        {
            if (param.type == AnimatorControllerParameterType.Trigger)
            {
                if (!triggerNames.Contains(param.name))
                {
                    triggerNames.Add(param.name);
                    Debug.Log($"- {param.name}");
                }
            }
        }
        for (int i = 0; i < toggles.Length; i++)
        {
            int index = i;
            Debug.Log(toggles[i].name);
            toggles[i].onValueChanged.AddListener((bool isOn) =>
            {
                if (isOn)
                {
                    PlayAnimation(index);
                }
            });
        }
    }

    public void PlayAnimation(int num)
    {
        Debug.Log(triggerNames[num].ToString());
        if(num < triggerNames.Count) animator.SetTrigger(triggerNames[num]);
    }
}
