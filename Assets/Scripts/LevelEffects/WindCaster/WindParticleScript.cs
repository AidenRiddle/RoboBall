using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class WindParticleScript : MonoBehaviour
{
    [SerializeField] VisualEffect VFX;
    [SerializeField] WindCaster conditionDictator;

    private void Update()
    {
        if (conditionDictator.IsBlowing) VFX.Play();
        else VFX.Stop();
    }
}
