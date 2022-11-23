using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectorRot : MonoBehaviour
{
    float rotSpeed = 50f;

    private bool isRotate = false;

    void Update()
    {
        if (isRotate)
        {
            transform.Rotate(rotSpeed * Time.deltaTime, 0, 0);
        }
    }

    public void StartRotate()
    {
        if (isRotate) return;

        isRotate = true;
    }

    public void StopRotate()
    {
        isRotate = false;
    }

}
