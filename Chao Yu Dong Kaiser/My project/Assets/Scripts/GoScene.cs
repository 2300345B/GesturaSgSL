using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoScene : MonoBehaviour
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
}
