using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ModelAnim : MonoBehaviour
{
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
    public void PlayAnimation4()
    {
        animator.SetTrigger("ILoveYou");

    }
    public void PlayAnimation5()
    {
        animator.SetTrigger("Please");

    }
    public void PlayAnimation6()
    {
        animator.SetTrigger("ThankYou");

    }
    public void PlayAnimation7()
    {
        animator.SetTrigger("Yes");

    }
    public void PlayAnimation8()
    {
        animator.SetTrigger("No");

    }
    // =====================================
    public void PlayAnimation(string letter)
    {
        animator.SetTrigger("Letter" + letter.ToUpper());
    }
}
