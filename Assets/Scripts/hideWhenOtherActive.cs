using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hideWhenOtherActive : MonoBehaviour
{
    public GameObject[] activators;
    public GameObject[] itemsToHide;

    void Update()
    {
        foreach (GameObject item in itemsToHide)
        {
            bool i = true;
            foreach (GameObject activator in activators) if (activator.activeSelf) i = false;
            item.SetActive(i);
        }
    }

    public static bool othersAreOrActive(params GameObject[] gameObjects)
    {
        if (gameObjects.Length <= 0) return false;
        foreach (GameObject o in gameObjects)
        {
            if (o.activeSelf) return true;
        }
        return false;
    }
}
