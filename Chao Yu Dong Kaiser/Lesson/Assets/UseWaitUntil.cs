using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UseWaitUntil : MonoBehaviour
{
    public bool MouseClicked = false;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start Coroutine");
        StartCoroutine("WaitForSelect1");
    }

    // Update is called once per frame
    IEnumerator WaitForSelect1()
    {
        yield return new WaitUntil(() => MouseClicked == true); 
        Debug.Log("MosueClicked is true "+Time.time);
    }
}
