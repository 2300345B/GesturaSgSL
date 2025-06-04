using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public UnityEvent OnDamaged;
    public int currentHealth = 5;    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            OnDamaged.Invoke();
    }

    public void DamagedHealth()
    {
        currentHealth -= 1;
    }
}
