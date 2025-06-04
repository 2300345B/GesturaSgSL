using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseWait : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("In Start()");
        StartCoroutine("Delay2s");
    }

    IEnumerator Delay2s()
    {
        Debug.Log("Start of delay");
        yield return new WaitForSeconds(2);
        Debug.Log("Aft 2sec " + Time.time);
    }
}
