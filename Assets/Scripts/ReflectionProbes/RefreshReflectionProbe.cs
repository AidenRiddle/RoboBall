using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefreshReflectionProbe : MonoBehaviour
{
    ReflectionProbe probe;

    //float timeSinceLastRefresh = 0;
    float refreshRate = 60;

    void Awake()
    {
        refreshRate = GameManager.ProbeRefreshRate;
        probe = GetComponent<ReflectionProbe>();
        StartCoroutine(RefreshProbe(refreshRate));
    }

    /*void Update()
    {
        timeSinceLastRefresh += Time.deltaTime;
        if (timeSinceLastRefresh >= refreshRate)
        {
            StartCoroutine(RefreshProbe());
            timeSinceLastRefresh = 0;
        }
    }*/

    IEnumerator RefreshProbe(float deltaTime)
    {
        /*probe.RenderProbe();
        yield return null;*/

        for (; ; )
        {
            probe.RenderProbe();
            yield return new WaitForSeconds(deltaTime);
        }
    }
}
