using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectorRot : MonoBehaviour
{
    float rotSpeed = 50f;
    //float time = 0;

    private bool isRotate = false;

    void Update()
    {
        //if (isRotate == false && Input.GetKeyDown(KeyCode.P))
        //{
        //    isRotate = true;
        //    time = 0f;
        //}

        if (isRotate)
        {
            //time += Time.deltaTime;
            //if (time > 40f)
            //{
            //    isRotate = false;
            //    return;
            //}

            transform.Rotate(rotSpeed * Time.deltaTime, 0, 0);
        }
    }

    public void StartRotate()
    {
        if (isRotate) return;

        isRotate = true;
        //time = 0f;
    }

    public void StopRotate()
    {
        isRotate = false;
    }

    //void rotate()
    //{
    //    time = 0;
        
    //    while(time <= 40)
    //    {
    //        time += Time.deltaTime;
    //        transform.Rotate(rotSpeed * Time.deltaTime, 0, 0);
    //    }
    //}
}
