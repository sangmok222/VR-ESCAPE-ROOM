using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFOV : MonoBehaviour
{
    [Range(0, 360)]
    public float viewAngle = 120f;//시야각
    public float viewRange = 15f;//시야범위

    [Header("플레이어 판단에 필요한 변수")]
    Transform enemyTr;
    Transform playerTr;
    int playerLayer;
    int obstacleLayer;
    int layerMask;

    private void Start()
    {
        enemyTr = GetComponent<Transform>();
        playerTr = GameObject.FindGameObjectWithTag("PLAYER").transform;
        playerLayer = LayerMask.NameToLayer("PLAYER");
        obstacleLayer = LayerMask.NameToLayer("OBSTACLE");
        //레이어 중첩
        //위에서 지정한 플레이어 레이어와 옵태클 레이어 두개를 OR 연산하여 병합한 레이어를 만듬
        //이전에 Barrel 때 사용했듯 레이어 설정을 통해 간섭 유무를 결정 할 수 있음.
        layerMask = 1 << playerLayer | 1 << obstacleLayer;
    }

    //원 위에 있는 점의 좌표값을 계산하는 메소드
    public Vector3 CirclePoint(float angle)
    {
        //트렌스폼의 오일러 각도를 가지고 온것은 로컬 좌표계 기준으로 설정하기 위하여 사용 Enemy의 y 회전값을 더함
        angle += transform.eulerAngles.y;
        //원 위의 좌표를 가져오기 위하여 삼각함수 활용
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));
    }
    //추적 유무를 판단하기 위한 메소드
    public bool isTracePlayer()
    {
        bool isTrace = false;
        Collider[] colls = Physics.OverlapSphere(enemyTr.position, viewRange, 1 << playerLayer);
        //플레이어 레이어를 검출하므로 1인 경우 = 플레이어가 범위에 들어 왔을 때
        if (colls.Length == 1)
        {                   // A위치 - B위치 = B에서 A를 가르키는 방향
            Vector3 dir = (playerTr.position - enemyTr.position).normalized;

            //Enemy 시야각에 플레이어가 위치하는지 판단
            if (Vector3.Angle(enemyTr.forward, dir) < viewAngle * 0.5)
            {
                isTrace = true; //존재 한다면 추적 가능
            }
        }
        return isTrace;
    }

    //플레이어를 직접적으로 보고 있는지
    //obtacle 레이어로 가려진 상태가 아닌지 판단
    public bool isViewPlayer()
    {
        bool isView = false;
        //레이캐스트 함수의 경우 기본 반환형은 bool임
        //하지만 out 키워드를 통해서 충돌 정보를 전달함 이때 전달 정보를 저장할 변수를 미리 선언해야함
        RaycastHit hit;
        Vector3 dir = (playerTr.position - enemyTr.position).normalized;
        if (Physics.Raycast(enemyTr.position,   //발사 위치
                            dir,    //발사 방향
                            out hit,    //충돌 정보 전달
                            viewRange,  //레이 길이
                            layerMask))    //검출 레이어
        {
            //검출된 오브젝트가 플레이어 태그를 가지고 있으면 True 아니면 false
            isView = hit.collider.CompareTag("PLAYER");
        }
        return isView;
    }



}
