using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//맵 형광등 달랑달랑 거리는 스크립트
public class FluorescentRot : MonoBehaviour
{
    Quaternion rot; 
    public float delta = 2f;//이동가능한 최대값
    public float speed = 3f;//이동속도

    void Start()
    {
        rot = transform.rotation;
    }

    void Update()
    {
        Quaternion v = rot;
        v.x += delta * Mathf.Sin(Time.time * speed);
        transform.rotation = v;
    }
}
