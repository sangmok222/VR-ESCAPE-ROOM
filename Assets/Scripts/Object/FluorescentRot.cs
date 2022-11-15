using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�� ������ �޶��޶� �Ÿ��� ��ũ��Ʈ
public class FluorescentRot : MonoBehaviour
{
    Quaternion rot; 
    public float delta = 2f;//�̵������� �ִ밪
    public float speed = 3f;//�̵��ӵ�

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
