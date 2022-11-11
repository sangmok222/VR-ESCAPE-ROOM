using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderEnemyAi : MonoBehaviour
{
    //Enemy ���¸� ǥ���ϱ� ���� ������ ���� ����
    public enum State
    {
        PATROL, TRACE, ATTACK, DIE
    }
    public State state = State.PATROL;
    Transform playerTr; //�÷��̾� ��ġ ���庯��
    Transform enemyTr;//Enemy ��ġ

    public float attackDisk = 5f;//���� �����Ÿ�
    public float traceDist = 10f;//���� �����Ÿ�

    // public bool isAttack = false;
    public bool isDie = false; //��� ���� �Ǵ�

    //�ð����� ����
    //�ڷ�ƾ �Լ� ������ �Լ��� ���۵� �� ��� ��ٸ� �� �ֵ���
    //�ð����� ������ Ȱ����
    WaitForSeconds ws;

    //Enemy �̵��� �����ϴ� ��ũ��Ʈ MoveAgent�� ����ϱ� ���� ����
    SpiderMoveAgent spiderMoveAgent;
    SpiderEnemyAttack spiderEnemyAttack;

    //Animator animator;

    //�ִϸ����� ��Ʈ�ѷ��� �ִ� �Ķ���� ���� �̸� ����
    //�Ķ������ ��ҹ��� �� ���� Ʋ���� �ȵ�
    //�̸� ��ǻ�Ϳ� �˷��ΰ� �ִϸ��̼� ������ �� ����Ͽ� �������
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

            //�÷��̾ �����Ѵٸ� �ش� ������Ʈ�� ���� Transform ����
            playerTr = player.GetComponent<Transform>();
        }
        enemyTr = GetComponent<Transform>();
        spiderMoveAgent = GetComponent<SpiderMoveAgent>();
        //animator = GetComponent<Animator>();
        spiderEnemyAttack = GetComponent<SpiderEnemyAttack>();

        //������ ������ �ð����� ������ 0.3�� ������ �ɵ��� ����
        //0.3�ʾ� �����̰� ����ٴ� �̾߱���
        ws = new WaitForSeconds(0.3f);

        //animator.SetFloat(hashOffset, Random.Range(0f, 1f));
        //animator.SetFloat(hashWalkSpeed, Random.Range(1f, 1.2f));

        enemyFov = GetComponent<EnemyFOV>();
    }

    private void OnEnable()
    {
        //CheckState�ڷ�ƾ �Լ� ȣ��
        StartCoroutine(CheckState());
        //StartCoroutine("CheckState");

        //Action �ڷ�ƾ �Լ� ȣ��
        StartCoroutine(Action());



    }

    //Enemy�� ���¸� �˻��ϴ� �ڷ�ƾ �Լ�
    IEnumerator CheckState()
    {
        //�ٸ� ��ũ��Ʈ �ʱ�ȭ (Awake, Start)�Լ� �� ���� ���
        yield return new WaitForSeconds(1f);

        //�Ϲ� �Լ��� �����ϰ� �����ֱ⸦ ���� ������ ���������� ���� ��Ű�� ���ؼ� while�� ���
        while (!isDie)
        {

            //+print("üũ������Ʈ");
            if (state == State.DIE)
                yield break;

            //�÷��̾�� ememy �Ÿ� ���
            float dist = Vector3.Distance(playerTr.position, enemyTr.position);
            if (dist <= attackDisk) //���ĵ� �� ��������
            {
                //���� ��Ÿ� �̳��̸鼭 �÷��̾ ���� ��, ��ֹ��� �߰��� ������ �Ǵ�
                if (enemyFov.isViewPlayer())
                {
                    state = State.ATTACK;
                }
                else
                    state = State.TRACE;
            }
            //���� �ݰ� �� �þ߰��� �ִ��� �Ǵ� �� ����
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
            //print("����");
            yield return ws;

            switch (state)
            {
                case State.PATROL:
                    spiderMoveAgent.isAttack = false;
                    spiderMoveAgent.PATROLLING = true;
                    //animator.SetBool(hashMove, true);
                    // animator.SetBool("isMove", ture);
                    break;
                case State.TRACE:
                    spiderMoveAgent.isAttack = false;
                    spiderMoveAgent.TRACETARGET = playerTr.position;
                    //animator.SetBool(hashMove, true);
                    break;
                case State.ATTACK:

                    spiderMoveAgent.Stop();
                    spiderMoveAgent.isAttack = true;

                    //animator.SetBool(hashMove, true);
                    if (spiderMoveAgent.isAttack == false)
                    {

                        spiderMoveAgent.isAttack = true;

                    }
                    break;
                case State.DIE:
                    //������ �±׸� �����ؼ� �������� ���� ��Ű��
                    this.gameObject.tag = "Untagged";
                    isDie = true;
                    spiderMoveAgent.isAttack = false;
                    spiderMoveAgent.Stop();

                    // animator.SetInteger(hashDieIdx, Random.Range(0, 3));
                    //animator.SetTrigger(hashDie);
                    //����� �ݶ��̴��� ��Ȱ��ȭ �Ͽ�
                    //���� �ڸ��� �Ѿ��� �浹�ϴ� ������ ����
                    GetComponent<CapsuleCollider>().enabled = false;

                    break;

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //NaveMeshAgent�� �ӵ��� ���������� �����Ͽ�
        //Animator�� Speed �Ķ���Ϳ� ����
        //�̵��ӵ��� ���߾� �ִϸ��̼� ��ȯ�� �Ͼ������
        //animator.SetFloat(hashSpeed, moveAgent.SPEED);
    }
    public void OnPlayerDie()
    {
        spiderMoveAgent.Stop();
        spiderMoveAgent.isAttack = false;
        //�������� ��� �ڷ�ƾ �Լ� ����
        StopAllCoroutines();

        //animator.SetTrigger(hashPlayerDie);
    }
}
