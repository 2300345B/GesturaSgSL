using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhraseHandler : MonoBehaviour
{
    public Dropdown phrasesDropdown;
    public Animator animator; // <-- You need to link your Animator here in the Inspector

    void Start()
    {
        phrasesDropdown.onValueChanged.AddListener(HandlePhraseSelected);
    }

    void HandlePhraseSelected(int index)
    {
        string selectedPhrase = phrasesDropdown.options[index].text;
        Debug.Log("Selected: " + selectedPhrase);

        // Call the correct animation method
        switch (index)
        {
            case 0:
                PlayAnimation1();
                break;
            case 1:
                PlayAnimation2();
                break;
            case 2:
                PlayAnimation3();
                break;
            default:
                Debug.LogWarning("Unknown option selected.");
                break;
        }
    }

    public void PlayAnimation1()
    {
        animator.SetTrigger("Hi");
    }

    public void PlayAnimation2()
    {
        animator.SetTrigger("Howareyou");
    }

    public void PlayAnimation3()
    {
        animator.SetTrigger("Sorry");
    }
}
