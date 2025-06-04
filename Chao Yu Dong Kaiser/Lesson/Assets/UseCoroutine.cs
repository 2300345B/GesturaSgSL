using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseCoroutine : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Calling coroutine in Start()");
        //start coroutine 1 or 2
        StartCoroutine("Routine1");

    }
    IEnumerator Routine1()
    {
        Debug.Log("Still in frame 1");
        yield return DoFirst();
        Debug.Log("Do First has ended");
    }
    IEnumerator DoFirst()
    {
        for (int i = 0; i < 5; i++) {
            Debug.Log("Iteration " + i);
    }
        yield return null;
    }
}
