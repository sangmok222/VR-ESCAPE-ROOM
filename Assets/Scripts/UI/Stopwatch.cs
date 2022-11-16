using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Stopwatch : MonoBehaviour
{
    public float curTime;
    bool stopwatchActive = false;

    public Text timeText;


    void Awake()
    {
        curTime = 0;
    }

    void Update()
    {
        if (stopwatchActive == true)
        {
            curTime = curTime + Time.deltaTime;
        }
        TimeSpan time = TimeSpan.FromSeconds(curTime);
        timeText.text = time.ToString(@"mm\:ss\:ff");
    }

    public void Startstopwatch()
    {
        stopwatchActive = true;
    }

    public void Stopstopwatch()
    {
        stopwatchActive = false;
    }

    public void ResetStopwatch()
    {

    }

}
