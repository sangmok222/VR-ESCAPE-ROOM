using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class SheetRackCaseAni : MonoBehaviour
{
    public float distance = 5f; //������ ����
    public RaycastHit rayHit; //Ray�� �¾Ҵٴ� �� �˷��ִ� ����
    public Ray ray; // ��� �� ���� ����
    bool open = true;
    public Animator ani;

    void Start()
    {
        ray = new Ray(); //new�� �̿��� ���̸� �ϳ� ����
        ani = GetComponent<Animator>();
        
        
    }

    void Update()
    {
        ray.origin = this.transform.position; //���� ��ġ�� ����� ������Ʈ ��ġ ����
        ray.direction = this.transform.forward;//���� ������ ����� ������Ʈ ���� ���� 

        

        if (Input.GetKeyDown(KeyCode.G)) //GŰ ������
        {                     //���� ��ġ,      ���� ����,     ���� ��ȯ, ���� ���� 
            if (Physics.Raycast(ray.origin, ray.direction, out rayHit, distance))
            {
                if (rayHit.collider.CompareTag("SHEET"))
                {
                    rayHit.transform.GetComponent<Animator>().SetBool("Open", open);
                    open = !open;
                }

            }
        }

      
    }

   


   




}
