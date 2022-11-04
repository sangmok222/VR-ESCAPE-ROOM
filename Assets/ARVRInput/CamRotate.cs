using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotate : MonoBehaviour
{
    Vector3 angle;
    public float sensitivity = 200;


    void Start()
    {
        angle.y = -Camera.main.transform.eulerAngles.x;
        angle.x = Camera.main.transform.eulerAngles.y;
        angle.z = Camera.main.transform.eulerAngles.z;
    }


    void Update()
    {
        //마우스 좌우 입력을 받아옴
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");

        //회전 값 누적
        //각도를 대입하여 변경하는 것이 아니라
        //현재 로컬 회전값에 더해주는 식으로 오브젝트를 회전
        angle.x += x * sensitivity * Time.deltaTime;
        angle.y += y * sensitivity * Time.deltaTime;

        transform.eulerAngles = new Vector3(-angle.y,
                                            angle.x,
                                            transform.eulerAngles.z);

    }
}
