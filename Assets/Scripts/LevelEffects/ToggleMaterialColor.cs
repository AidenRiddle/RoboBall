using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleMaterialColor : MonoBehaviour
{
    [SerializeField] Material material;
    [SerializeField] WindCaster conditionDictator;
    [SerializeField] Color targetColor = Color.cyan;
    Color originalColor = new Color(1, 0.5f, 0);

    private void Update()
    {
        if (conditionDictator.IsBlowing) material.SetColor("_Color", targetColor);
        else material.SetColor("_Color", originalColor);
    }

    private void OnApplicationQuit()
    {
        material.SetColor("_Color", originalColor);
    }
}
