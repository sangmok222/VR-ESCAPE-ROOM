using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Projector : MonoBehaviour
{
    public VideoPlayer video;
    public Light light;
    public GameObject projector;
    

    private void Start()
    {
        video = GetComponent<VideoPlayer>();
        light = GetComponent<Light>();

        projector = GetComponent<GameObject>();
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            VideoPlay();
            LightStart();
        }

    }




    void VideoPlay()
    {
        GetComponentInChildren<VideoPlayer>().Play();
        projector.SetActive(true);
    }

    void LightStart()
    {
        GetComponentInChildren<Light>();
        light.gameObject.SetActive(true);


    }
}




