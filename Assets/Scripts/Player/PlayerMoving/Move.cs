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

        //    // Space.Self�� ���� Rotate() ���� ����
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
        
        //// ���콺 ����
        currentRot -= rotX;
        
        //// ���콺�� Ư�� ������ �Ѿ�� �ʰ� ����ó��
        currentRot = Mathf.Clamp(currentRot, -80f, 80f);
        
        //// Camera�� Player�� �ڽ��̹Ƿ� �÷��̾��� Y�� ȸ���� Camera���Ե� �Ȱ��� �����
        this.transform.localRotation *= Quaternion.Euler(0, rotY, 0);
        //// Camera�� transform ������Ʈ�� ���÷����̼��� ���Ϸ����� 
        //// ����X�� �����̼��� ��Ÿ���� ���Ϸ����� �Ҵ����ش�.
        ////lightCam.transform.localEulerAngles = new Vector3(currentRot, 0f, 0f);

    }

}