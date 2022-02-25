using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindIndicator : MonoBehaviour
{
    [SerializeField]
    WindCaster conditionDictator;

    private void Update()
    {
        if (conditionDictator.IsBlowing) transform.rotation = Quaternion.Euler(0, 0, 90);
        else transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
