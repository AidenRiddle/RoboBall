using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerActivation : Killable
{
    public GameObject[] targetGameObjects;
    public bool setObjectActiveState = false;
    public bool invertState = false;

    protected override void OnDeath()
    {
        if (invertState)
        {
            foreach (GameObject go in targetGameObjects)
            {
                go.SetActive(!go.activeSelf);
            }
        }
        else
        {
            foreach (GameObject go in targetGameObjects)
            {
                go.SetActive(setObjectActiveState);
            }
        }
    }
}
