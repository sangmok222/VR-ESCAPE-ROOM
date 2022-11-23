using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class SheetRackCaseAni : MonoBehaviour
{
    private Camera playerCam;
    
    public float distance = 5f; //레이의 길이
    public RaycastHit rayHit; //Ray가 맞았다는 걸 알려주는 변수
    public Ray ray; // 사용 될 레이 변수
    public Animator ani;
    
    bool open = true;

    void Start()
    {
        playerCam = Camera.main;//게임이 시작 되면 활성화된 메인 카메라를 가져온다.
        ray = new Ray(); //new를 이용해 레이를 하나 생성
        ani = GetComponent<Animator>();
        
        
    }

    void Update()
    {

        //카메라의 중앙을 기준으로 레이를 쏜다.
        //rayOrigin : 광선이 날아갈 정중앙위치, 
        //ViewportToWorldPoint : 카메라의 한 지점의 위치가 게임상에서
        //어느 위치(가로 중앙, 세로 중, 카메라의 깊이)를 리턴
        Vector3 rayOrigin = playerCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));

        Vector3 rayDir = playerCam.transform.forward; //카메라의 정면을 기준으로 레이를 쏜다.

        Ray ray = new Ray(rayOrigin, rayDir);
        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);

        if (Input.GetKeyDown(KeyCode.G))
        {

            //레이캐스트는 물리처리 이므로 pHysics의 함수
            //Raycase(광선 시작지점, 방향, 거리)
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
