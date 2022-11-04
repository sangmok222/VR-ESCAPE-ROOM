using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Rigidbody playerRigidbody;
    public Camera fpsCam;

    public float MoveSpeed;
    float rotSpeed;
    float currentRot;

    // Start is called before the first frame update
    void Start()
    {
        MoveSpeed = 7.0f;
        rotSpeed = 3.0f;
        currentRot = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        RotCtrl();
    }

    void PlayerMove()
    {
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        float xSpeed = xInput * MoveSpeed;
        float zSpeed = zInput * MoveSpeed;

        transform.Translate(Vector3.forward * zSpeed * Time.deltaTime);
        transform.Translate(Vector3.right * xSpeed * Time.deltaTime);
    }

    void RotCtrl()
    {
        float rotX = Input.GetAxis("Mouse Y") * rotSpeed;
        float rotY = Input.GetAxis("Mouse X") * rotSpeed;

        // ���콺 ����
        currentRot -= rotX;

        // ���콺�� Ư�� ������ �Ѿ�� �ʰ� ����ó��
        currentRot = Mathf.Clamp(currentRot, -80f, 80f);

        // Camera�� Player�� �ڽ��̹Ƿ� �÷��̾��� Y�� ȸ���� Camera���Ե� �Ȱ��� �����
        this.transform.localRotation *= Quaternion.Euler(0, rotY, 0);
        // Camera�� transform ������Ʈ�� ���÷����̼��� ���Ϸ����� 
        // ����X�� �����̼��� ��Ÿ���� ���Ϸ����� �Ҵ����ش�.
        fpsCam.transform.localEulerAngles = new Vector3(currentRot, 0f, 0f);
    }
}