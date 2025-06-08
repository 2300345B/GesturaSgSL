using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureController : MonoBehaviour
{
    public Animator handAnimator;

    void OnGestureRecognized(string gesture)
    {
        // Trigger animation based on gesture
        handAnimator.SetTrigger(gesture);
    }


// Update is called once per frame
void Update()
    {
        
    }
}
