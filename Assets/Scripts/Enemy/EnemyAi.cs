using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    //Enemy 상태를 표현하기 위해 열거형 변수 정의
    public enum State
    {
        PATROL, TRACE, ATTACK, DIE
    }
    public State state = State.PATROL;
    Transform playerTr; //플레이어 위치 저장변수
    Transform enemyTr;//Enemy 위치

    public float attackDisk = 5f;//공격 사정거리
    public float traceDist = 10f;//추적 사정거리

   // public bool isAttack = false;
    public bool isDie = false; //사망 유무 판단

    //시간지연 변수
    //코루틴 함수 내에서 함수가 동작될 때 잠깐 기다릴 수 있도록
    //시간지연 변수를 활용함
    WaitForSeconds ws;

    //Enemy 이동을 관리하는 스크립트 MoveAgent를 사용하기 위한 변수
    MoveAgent moveAgent;
    EnemyAttack enemyAttack;

    //Animator animator;

    //애니메이터 컨트롤러에 있는 파라메터 값을 미리 매핑
    //파라매터의 대소문자 및 글자 틀리면 안됨
    //미리 컴퓨터에 알려두고 애니메이션 제어할 때 사용하여 성능향상
   // readonly int hashMove = Animator.StringToHash("isMove");
    //readonly int hashSpeed = Animator.StringToHash("Speed");
    //readonly int hashDie = Animator.StringToHash("Die");
    //readonly int hashDieIdx = Animator.StringToHash("DieIdx");

    //readonly int hashOffset = Animator.StringToHash("Offset");
    //readonly int hashWalkSpeed = Animator.StringToHash("WalkSpeed");
    //readonly int hashPlayerDie = Animator.StringToHash("PlayerDie");

    EnemyFOV enemyFov;


    private void Awake()
    {
        var player = GameObject.FindGameObjectWithTag("PLAYER");
        if (player != null)
        {

            //플레이어가 존재한다면 해당 오브젝트로 부터 Transform 추출
            playerTr = player.GetComponent<Transform>();
        }
        enemyTr = GetComponent<Transform>();
        moveAgent = GetComponent<MoveAgent>();
        //animator = GetComponent<Animator>();
        enemyAttack = GetComponent<EnemyAttack>();

        //위에서 선언한 시간지연 변수에 0.3초 딜레이 걸도록 설정
        //0.3초씩 딜레이가 생긴다는 이야기임
        ws = new WaitForSeconds(0.3f);

        //animator.SetFloat(hashOffset, Random.Range(0f, 1f));
        //animator.SetFloat(hashWalkSpeed, Random.Range(1f, 1.2f));

        enemyFov = GetComponent<EnemyFOV>();
    }

    private void OnEnable()
    {
        //CheckState코루틴 함수 호출
        StartCoroutine(CheckState());
        //StartCoroutine("CheckState");

        //Action 코루틴 함수 호출
        StartCoroutine(Action());

        

    }
   
    //Enemy의 상태를 검사하는 코루틴 함수
    IEnumerator CheckState()
    {
        //다른 스크립트 초기화 (Awake, Start)함수 돌 동안 대기
        yield return new WaitForSeconds(1f);

        //일반 함수와 동일하게 생명주기를 가짐 때문에 지속적으로 동작 시키기 위해서 while문 사용
        while (!isDie)
        {

            //+print("체크스테이트");
            if (state == State.DIE)
                yield break;

            //플레이어와 ememy 거리 계산
            float dist = Vector3.Distance(playerTr.position, enemyTr.position);
            if (dist <= attackDisk) //겹쳐도 됨 성능차이
            {
                //공격 사거리 이내이면서 플레이어를 직시 즉, 장애물이 중간에 없는지 판단
                if (enemyFov.isViewPlayer())
                {
                    state = State.ATTACK;
                }
                else
                    state = State.TRACE;
            }
            //추적 반경 및 시야각에 있는지 판단 후 추적
            else if (enemyFov.isTracePlayer())
            {
                state = State.TRACE;
            }
            else
            {
                state = State.PATROL;
            }
            yield return ws;
        }
    }

    IEnumerator Action()
    {
        while (!isDie)
        {
            //print("엑션");
            yield return ws;

            switch (state)
            {
                case State.PATROL:
                    moveAgent.isAttack = false;
                    moveAgent.PATROLLING = true;
                    //animator.SetBool(hashMove, true);
                    // animator.SetBool("isMove", ture);
                    break;
                case State.TRACE:
                    moveAgent.isAttack = false;
                    moveAgent.TRACETARGET = playerTr.position;
                    //animator.SetBool(hashMove, true);
                    break;
                case State.ATTACK:
                    
                    moveAgent.Stop();
                    moveAgent.isAttack = true;
                    
                    //animator.SetBool(hashMove, true);
                    if (moveAgent.isAttack == false)
                    {
                        
                        moveAgent.isAttack = true;

                    }
                    break;
                case State.DIE:
                    //죽으면 태그를 변경해서 수량에서 제외 시키기
                    this.gameObject.tag = "Untagged";
                    isDie = true;
                    moveAgent.isAttack = false;
                    moveAgent.Stop();

                   // animator.SetInteger(hashDieIdx, Random.Range(0, 3));
                    //animator.SetTrigger(hashDie);
                    //사망후 콜라이더를 비활성화 하여
                    //죽은 자리에 총알이 충돌하는 현상을 제거
                    GetComponent<CapsuleCollider>().enabled = false;

                    break;

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //NaveMeshAgent의 속도를 지속적으로 갱신하여
        //Animator의 Speed 파라미터에 전달
        //이동속도에 맞추어 애니메이션 전환이 일어나도록함
        //animator.SetFloat(hashSpeed, moveAgent.SPEED);
    }
    public void OnPlayerDie()
    {
        moveAgent.Stop();
        moveAgent.isAttack = false;
        //동작중인 모든 코루틴 함수 종료
        StopAllCoroutines();

        //animator.SetTrigger(hashPlayerDie);
    }
}
