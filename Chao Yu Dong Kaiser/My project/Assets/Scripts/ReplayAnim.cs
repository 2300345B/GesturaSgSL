using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayAnim : MonoBehaviour
{
    public Animator animator;      // Reference to the Animator
    public string Hi;   // Name of the animation state

    public void Replay()
    {
        animator.Play(Hi, -1, 0f);  // Replays from start
    }
}