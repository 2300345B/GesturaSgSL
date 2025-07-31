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

    public void PlayAnimationA()
    {
        animator.SetTrigger("LetterA");

    }
    public void PlayAnimationB()
    {
        animator.SetTrigger("LetterB");

    }
    public void PlayAnimationC()
    {
        animator.SetTrigger("LetterC");

    }
    public void PlayAnimationD()
    {
        animator.SetTrigger("LetterD");

    }
    public void PlayAnimationE()
    {
        animator.SetTrigger("LetterE");

    }
    public void PlayAnimationF()
    {
        animator.SetTrigger("LetterF");

    }
    public void PlayAnimationG()
    {
        animator.SetTrigger("LetterG");

    }
    public void PlayAnimationH()
    {
        animator.SetTrigger("LetterH");

    }
}
