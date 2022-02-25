using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    [SerializeField] private int updateFrequency = 10;    //Updates per second

    [SerializeField] private bool logAvgFrameRate = false;
    [SerializeField] private bool logFrameRatePeak = false;

    private int totalFrames = 0;
    private int framesCounted = 0;
    private float timeSinceLastFrame = 0;

    private int peakFrameRate = 0;

    private StringBuilder sb = new StringBuilder();

    // Update is called once per frame
    void Update()
    {
        framesCounted++;
        timeSinceLastFrame += Time.deltaTime;
        if (timeSinceLastFrame >= (1f / updateFrequency))
        {
            int fps = (int)(framesCounted / timeSinceLastFrame);
            sb.Clear();
            sb.Append(fps);
            sb.Append(" FPS");
            gameObject.GetComponent<Text>().text = sb.ToString();
            totalFrames += framesCounted;
            framesCounted = 0;
            timeSinceLastFrame = 0;

            if (logFrameRatePeak)
            {
                UpdateHighestFPSPeak(fps);
            }
        }
    }

    void UpdateHighestFPSPeak(int currentValue)
    {
        if (currentValue > peakFrameRate) peakFrameRate = currentValue;
    }

    private void OnApplicationQuit()
    {
        if (logAvgFrameRate)
        {
            Debug.Log("<color=lime>Average FrameRate:</color> " + (totalFrames / Time.timeSinceLevelLoad));
        }
        if (logFrameRatePeak)
        {
            Debug.Log("FrameRate Peak (Highest): " + peakFrameRate);
        }
    }
}
