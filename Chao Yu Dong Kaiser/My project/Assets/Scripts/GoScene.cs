using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoScene : MonoBehaviour
{
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
}
