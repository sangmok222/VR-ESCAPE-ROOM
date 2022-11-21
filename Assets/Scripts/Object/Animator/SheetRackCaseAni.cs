using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class SheetRackCaseAni : MonoBehaviour
{
    public float distance = 5f; //레이의 길이
    public RaycastHit rayHit; //Ray가 맞았다는 걸 알려주는 변수
    public Ray ray; // 사용 될 레이 변수
    bool open = true;
    public Animator ani;

    void Start()
    {
        ray = new Ray(); //new를 이용해 레이를 하나 생성
        ani = GetComponent<Animator>();
        
        
    }

    void Update()
    {
        ray.origin = this.transform.position; //레이 위치를 사용할 오브젝트 위치 적용
        ray.direction = this.transform.forward;//레이 방향을 사용할 오브젝트 방향 적용 

        

        if (Input.GetKeyDown(KeyCode.G)) //G키 누르면
        {                     //레이 위치,      레이 방향,     레이 반환, 레이 길이 
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
