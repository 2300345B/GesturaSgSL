using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectNavigator : MonoBehaviour
{
    public List<GameObject> objects; // Drag your objects in the inspector
    public int visibleCount = 1;     // How many objects shown at once
    private int currentIndex = 0;

    void Start()
    {
        UpdateVisibleObjects();
    }

    public void Next()
    {
        if (currentIndex + visibleCount < objects.Count)
        {
            currentIndex++;
            UpdateVisibleObjects();
        }
    }

    public void Back()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            UpdateVisibleObjects();
        }
    }

    void UpdateVisibleObjects()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            if (i >= currentIndex && i < currentIndex + visibleCount)
                objects[i].SetActive(true);
            else
                objects[i].SetActive(false);
        }
    }
}
