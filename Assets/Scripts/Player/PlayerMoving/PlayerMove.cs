using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5f;
    //별도의 콜라이더와 리지드바디가 없어도
    //오브젝트끼리의 충돌체크 가능함
    //다만 중력 자체는 별도로 설정이 필요
    //물리엔진이 있는 것이 아니므로 스크립트로 제어해줘야함
    CharacterController cc;

    public float gravity = -20f;
    float yVelocity = 0;
    public float jumpPower = 5;

    void Start()
    {
        cc = GetComponent<CharacterController>();
    }


    void Update()
    {
        float h = ARVRInput.GetAxis("Horizontal");
        float v = ARVRInput.GetAxis("Vertical");
        Vector3 dir = new Vector3(h, 0, v);

        print(dir);

        //사용자의 방향을 카메라가 바라보는 방향으로
        //즉 사용자가 바라보는 방향으로 벡터값 변경
        //dir 방향을 메인 카메라가 바라보는 방향으로 변경
        dir = Camera.main.transform.TransformDirection(dir);


        //중력구현
        yVelocity += gravity * Time.deltaTime;
        dir.y = yVelocity;

        //바닥에 존재할 경우 위에서 계산된 중력 가속도를 0으로 설정
        if (cc.isGrounded)
        {
            yVelocity = 0;
        }
        if (ARVRInput.GetDown(ARVRInput.Button.Two, ARVRInput.Controller.RTouch))
        {
            yVelocity = jumpPower;
        }
        dir.y = yVelocity;

        cc.Move(dir * speed * Time.deltaTime);
    }
}
