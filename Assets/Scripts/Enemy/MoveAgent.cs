using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[System.Serializable]//직렬화 어트리뷰트
public class EnemyAnim
{
    public AnimationClip idle;
    public AnimationClip Attack1;
    public AnimationClip Attack2;
    public AnimationClip Run;
    public AnimationClip Walk;
    public AnimationClip Death;

}
//유니티에서 사용하는 애니메이션의 종류는 2가지
//Legacy(구형), Mecanim(Generic, Humonoid) 타입이 존재한다.
//Legacy - 이전 방식의 애니메이션 제어
//Generic - 메카님애니메이션, 사람 모양외의 모델에 적용, 리타겟팅 x
//Humanoid - 메카님애니메이션, 사람 모양의 모델에 적용, 리타겟팅 o
//리타겟팅이란 비슷한 관절부위를 통해서 하나의 애니메이션으로 여러 모델에 적용가능한것

//해당 스크립트를 동작시키기 위해서 필수적인 컴포넌트 지정
[RequireComponent(typeof(NavMeshAgent))]
public class MoveAgent : MonoBehaviour
{
    //인스펙터 뷰에 표시할 애니메이션 클래스 변수
    public EnemyAnim enemyAnim;
    public Animation anim; //Animation 컴포넌트 변수

    //순찰 지점을 저장하기 위한 리스트 타입 변수
    //리스트는 배열과 유사한 자료구조
    //배열과 차이는 가변길이이다.
    //데이터가 들어오면 길이가 늘어나고, 삭제되면 줄어듬
    public List<Transform> wayPoints;//Generic 소속이다.
    public int nextIdx; //다음 순찰 지점의 배열 Index

    NavMeshAgent agent;

    readonly float patrolSpeed = 1.5f;//순찰속도
    readonly float traceSpeed = 4f;//추적속도



    float damping = 1f;//회전값 속도 조절 계수
    Transform playerTr;
    Transform enemyTr;
    float nextAttack = 0f;
    readonly float AttackRate = 0.1f;
    

    public bool isAttack = false; //발사 유무 판단변수
    public AudioClip AttackSfx;




    private bool _patrolling;
    //프로퍼티 = 메소드의 형태이나 변수처럼 동작하는 놈
    //데이터의 기본적인 보안과 데이터 입/출 상황을 컨트롤할 때 사용
    public bool PATROLLING
    {
        get
        {
            return _patrolling;
        }
        set
        {
            _patrolling = value;
            if (_patrolling)
            {
                agent.speed = patrolSpeed;
                //순찰모드일 때 회전계수
                damping = 1f; //순찰모드일때는 좀 느긋하게
                MoveWayPoint();
            }
        }
    }

    Vector3 _traceTarget;
    public Vector3 TRACETARGET //프로퍼티
    {
        get
        {
            return _traceTarget;
        }
        set
        {
            _traceTarget = value;
            agent.speed = traceSpeed;
            damping = 7f;//Trace모드일때 순찰모드보다 7배 빠르게 몸을 회전 시킬 수 있다.

            //추적 대상 지정하는 함수 호출
            TraceTarget(_traceTarget);
        }
    }

    //이동속도에 대한 프로퍼티
    public float SPEED
    {
        //get만 정의하여 값을 설정할 수는 없음
        //가지고와서 사용만 가능
        get { return agent.velocity.magnitude; }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerTr = GameObject.FindGameObjectWithTag("PLAYER").GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        //목적지에 도달할 수록 속도를 줄이는 기능을 끈다.
        //본게임에서 Enemy는 플레이어를 추적하므로
        //속도를 줄이지 않도록 한다.
        agent.autoBraking = false;
        agent.speed = patrolSpeed;

        enemyTr = GetComponent<Transform>();
        //NavMeshAgent가 회전값 자동 갱신하는 부분 끄기
        agent.updateRotation = false;
        anim = GetComponent<Animation>();
        anim.clip = enemyAnim.idle; //게임 시작시 재생할 클립설정
        anim.Play();



        //하이어라키에서 WayPointGroup이라는 이름을 가지는 오브젝트를 검색하여 추출
        //Finde 함수는 성능이 굉장히 안좋기 때문에 업데이트 함수내에서 FInd를 호출하는 일은 피해가자
        var group = GameObject.Find("WayPointGroup");
        //null 체크를 통해서 해당 오브젝트가 존재하는지 체크
        if (group != null)
        {   //추출한 WayPointGroup 라는 이름의 오브젝트에서
            //자식 오브젝트의 Tranfrom 값 모두 추출함
            //이후에 wayPoints라는 리스트에 추가해줌 -> 위에서 만든 리스트 배열
            //GetComponentsInChildern 메소드
            //자기 자신을 포함하여 하위에 존재하는 모든 오브젝트를 가져옴
            group.GetComponentsInChildren<Transform>(wayPoints); //자식오브젝트를 싹 다 땡겨옴
            //리스트에서 0번째 인덱스에 해당하는 요소 삭제
            wayPoints.RemoveAt(0);//부모 오브젝트도 같이 포함되기 떄문에 0번쨰 오브젝트도 삭제시켜준다.

            //다음 순찰지역을 랜덤한 위치로 변경
            nextIdx = Random.Range(0, wayPoints.Count);
        }

        //목적지 이동시키는 함수 호출

        //MoveWayPoint();
        
        PATROLLING = true;

    }

    void MoveWayPoint()
    {
        //isPathStale 경로 계산 유무를 반환하는 함수
        //경로 계산중이라면 목적지 이동을 하지 않음
        //경로 계산이 끝나면 그 떄서야 이동하도록 함
        if (agent.isPathStale)//bool 자료형 0,1 을 반환하는 자료형이다. 즉 계산중일떄는 참이기때문에 리턴하고 계산이 끝나면 아래 코드를 실행한다.
            return;
        //계산이 끝난 후에 다음 위치로 이동
        agent.destination = wayPoints[nextIdx].position;
        //실제 이동 시작
        agent.isStopped = false;
        anim.CrossFade(enemyAnim.Walk.name, 0.3f);
    }

    void TraceTarget(Vector3 pos)
    {
        //경로 계산중일때는 패스
        if (agent.isPathStale)
            return;

        agent.destination = pos;
        agent.isStopped = false;
        anim.CrossFade(enemyAnim.Run.name, 0.3f);

    }
    public void Stop()
    {
        //에이전트 움직임 멈추기
        agent.isStopped = true;
        //혹시나 움직일지 모르기 때문에 남은속도 0으로 변경
        agent.velocity = Vector3.zero;
        _patrolling = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        //적이 이동중일때 실행됨
        if (agent.isStopped == false)
        {
            //NavMeshAgent 가 진행해야될 방향을 구함
            Quaternion rot = Quaternion.LookRotation(agent.desiredVelocity); //desiredVelocity 내가 원하는 방향으로 휙 튼다

            enemyTr.rotation = Quaternion.Slerp(enemyTr.rotation, rot, Time.deltaTime * damping);//카메라 때랑 비슷함
        }


        //순찰 모드가 아닐 경우에 아래 코드 수행안함
        //순찰 모드일 떄만 다음 순찰 지역계산하는 코드 수행
        if (!_patrolling)
            //return;
        //agent.velocity.magnitude = NavMesh의 속도 //10을 넣으면 내부적으로 루트 10의 연산을 한다.
        //agent.velocity.sqrMagnitude는 제곱근 연산을 미리해줘서 성능을 좋게 만든다.
        //해당조건은 아직 움직이는 중인데 목적지에 다와가는 상황을 야기함
        if (agent.velocity.sqrMagnitude >= 0.2f * 0.2f && agent.remainingDistance <= 0.5f)
        {

            nextIdx++;
            //nextIdx 값을 로테이션 시키기 위해서 사용
            //ex) 0%3 = 0, 1%3 = 1 ...3%3 = 0
            //위와 같이 뒤에 나머지연산하는 갯수만큼 돌고나면 다시 0으로 돌아오면서 로테이션 돌게된다.
            nextIdx = nextIdx % wayPoints.Count;

            nextIdx = Random.Range(0, wayPoints.Count);
            MoveWayPoint();
        }
        //재장전이 아니면서 공격 가능할 때만
        if (isAttack == true )
        {
            //Time.time 게임이 실행된 후 경과 시간

            //다음 발사시간 = 현재시간 + 발사간격 + 랜덤한 0 ~ 0.3초
           
          
           
            //공격함수 호츨
            Attack();

            nextAttack = Time.time + AttackRate + Random.Range(0f, 0.3f);
           
            //                                               A                  B
            Quaternion rot = Quaternion.LookRotation(playerTr.position - enemyTr.position);

            //벡터의 빼기 = A 벡터 - B 벡터 = B에서 A가 있는 위치를 가르킴
            //즉, enemyTr에서 playerTr을 가르키는 벡터
            enemyTr.rotation = Quaternion.Slerp(enemyTr.rotation, rot, Time.deltaTime * damping);
        }

    }
    void Attack()
    {
        //animator.SetTrigger(hashFire);
        //audio.PlayOneShot(AttackSfx, 1f);
        anim.CrossFade(enemyAnim.Attack1.name, 0.3f);

    }
}