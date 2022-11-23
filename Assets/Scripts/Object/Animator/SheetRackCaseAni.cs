using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class SheetRackCaseAni : MonoBehaviour
{
    private Camera playerCam;
    
    public float distance = 5f; //������ ����
    public RaycastHit rayHit; //Ray�� �¾Ҵٴ� �� �˷��ִ� ����
    public Ray ray; // ��� �� ���� ����
    public Animator ani;
    
    bool open = true;

    void Start()
    {
        playerCam = Camera.main;//������ ���� �Ǹ� Ȱ��ȭ�� ���� ī�޶� �����´�.
        ray = new Ray(); //new�� �̿��� ���̸� �ϳ� ����
        ani = GetComponent<Animator>();
        
        
    }

    void Update()
    {

        //ī�޶��� �߾��� �������� ���̸� ���.
        //rayOrigin : ������ ���ư� ���߾���ġ, 
        //ViewportToWorldPoint : ī�޶��� �� ������ ��ġ�� ���ӻ󿡼�
        //��� ��ġ(���� �߾�, ���� ��, ī�޶��� ����)�� ����
        Vector3 rayOrigin = playerCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));

        Vector3 rayDir = playerCam.transform.forward; //ī�޶��� ������ �������� ���̸� ���.

        Ray ray = new Ray(rayOrigin, rayDir);
        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);

        if (Input.GetKeyDown(KeyCode.G))
        {

            //����ĳ��Ʈ�� ����ó�� �̹Ƿ� pHysics�� �Լ�
            //Raycase(���� ��������, ����, �Ÿ�)
            if (Physics.Raycast(rayOrigin, rayDir, distance))
            {
                if (rayHit.collider.gameObject.tag == "SHEET")

                {
                    ani.SetBool("Open", true);
                }
            }
            
        }

    }


    
   


            




}
