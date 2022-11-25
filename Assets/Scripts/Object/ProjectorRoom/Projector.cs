using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Projector : MonoBehaviour
{
    public VideoPlayer video;
    public Light light;
    public GameObject projector;

    public float rotSpeed = 5f;

    public ProjectorRot cubeRot = null;
    public ProjectorRot cubeRot1 = null;


    



    private void Start()
    {
        //video = GetComponent<VideoPlayer>();
        //light = GetComponentInChildren<Light>();

        //projector = GetComponent<GameObject>();
        
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            VideoPlay();
            LightStart();
            cubeRot.StartRotate();
            cubeRot1.StartRotate();
            Invoke("ExitProjector", 31f);
        }

    }
    void VideoPlay()
    {
        
            video.Play();
    }
    void LightStart()
    {
        if (light.enabled == false)
        {
            light.enabled = true;
        }
        else
        {
            light.enabled = false;
        }
        
    }
    public void ExitProjector()
    {
        video.enabled = false;
        light.enabled = false;
        cubeRot.StopRotate();
        cubeRot1.StopRotate();
    }

    
}




