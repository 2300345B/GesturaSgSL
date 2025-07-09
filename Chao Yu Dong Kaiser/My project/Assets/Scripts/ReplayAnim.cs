using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayAnim : MonoBehaviour
{
    public Animator animator;     
    public string Hi; 

    public void Replay()
    {
        animator.Play(Hi, -1, 0f); 
    }
}