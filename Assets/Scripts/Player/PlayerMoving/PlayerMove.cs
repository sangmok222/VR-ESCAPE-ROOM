using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5f;
    //������ �ݶ��̴��� ������ٵ� ���
    //������Ʈ������ �浹üũ ������
    //�ٸ� �߷� ��ü�� ������ ������ �ʿ�
    //���������� �ִ� ���� �ƴϹǷ� ��ũ��Ʈ�� �����������
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

        //������� ������ ī�޶� �ٶ󺸴� ��������
        //�� ����ڰ� �ٶ󺸴� �������� ���Ͱ� ����
        //dir ������ ���� ī�޶� �ٶ󺸴� �������� ����
        dir = Camera.main.transform.TransformDirection(dir);


        //�߷±���
        yVelocity += gravity * Time.deltaTime;
        dir.y = yVelocity;

        //�ٴڿ� ������ ��� ������ ���� �߷� ���ӵ��� 0���� ����
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
