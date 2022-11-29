using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private Rigidbody playerRigidbody;


    public float MoveSpeed;
    public float rotSpeed;
    public float currentRot;



    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        MoveSpeed = 3.0f;
        rotSpeed = 3.0f;
        currentRot = 0f;
    }


    void Update()
    {
        PlayerMove();
        RotCtrl();

        //if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickLeft))
        //{
        //    Vector3 eulerAngle = new Vector3(0f, 2f, 0f);

        //    transform.Rotate(eulerAngle, Space.Self);

        //    // Space.Self일 때의 Rotate() 내부 구현
        //    transform.localRotation *= Quaternion.Euler(eulerAngle);

        //}
    }

    void PlayerMove()
    {
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        float xSpeed = xInput * MoveSpeed;
        float zSpeed = zInput * MoveSpeed;

        transform.Translate(Vector3.forward.normalized * zSpeed * Time.deltaTime, Space.Self);
        transform.Translate(Vector3.right.normalized * xSpeed * Time.deltaTime, Space.Self);
    }

    void RotCtrl()
    {
        float rotX = Input.GetAxis("Mouse Y") * rotSpeed;
        float rotY = Input.GetAxis("Mouse X") * rotSpeed;
        
        //// 마우스 반전
        currentRot -= rotX;
        
        //// 마우스가 특정 각도를 넘어가지 않게 예외처리
        currentRot = Mathf.Clamp(currentRot, -80f, 80f);
        
        //// Camera는 Player의 자식이므로 플레이어의 Y축 회전은 Camera에게도 똑같이 적용됨
        this.transform.localRotation *= Quaternion.Euler(0, rotY, 0);
        //// Camera의 transform 컴포넌트의 로컬로테이션의 오일러각에 
        //// 현재X축 로테이션을 나타내는 오일러각을 할당해준다.
        ////lightCam.transform.localEulerAngles = new Vector3(currentRot, 0f, 0f);

    }

}