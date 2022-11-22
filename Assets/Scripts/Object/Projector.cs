using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Projector : MonoBehaviour
{
    public VideoPlayer video;
    public Light light;
    public GameObject projector;
    public GameObject[] ProjectorRot;

    public float rotSpeed = 5f;
    

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
            Invoke("ExitProjector", 40f);
            ProjectorRotate();
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
    }

    void ProjectorRotate()
    {
        while (true)
        {
            projector.transform.Rotate(Vector3.up * rotSpeed * Time.deltaTime);
        }
    }
}




