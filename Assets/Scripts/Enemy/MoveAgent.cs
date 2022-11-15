using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[System.Serializable]//����ȭ ��Ʈ����Ʈ
public class EnemyAnim
{
    public AnimationClip idle;
    public AnimationClip Attack1;
    public AnimationClip Attack2;
    public AnimationClip Run;
    public AnimationClip Walk;
    public AnimationClip Death;

}
//����Ƽ���� ����ϴ� �ִϸ��̼��� ������ 2����
//Legacy(����), Mecanim(Generic, Humonoid) Ÿ���� �����Ѵ�.
//Legacy - ���� ����� �ִϸ��̼� ����
//Generic - ��ī�Ծִϸ��̼�, ��� ������ �𵨿� ����, ��Ÿ���� x
//Humanoid - ��ī�Ծִϸ��̼�, ��� ����� �𵨿� ����, ��Ÿ���� o
//��Ÿ�����̶� ����� ���������� ���ؼ� �ϳ��� �ִϸ��̼����� ���� �𵨿� ���밡���Ѱ�

//�ش� ��ũ��Ʈ�� ���۽�Ű�� ���ؼ� �ʼ����� ������Ʈ ����
[RequireComponent(typeof(NavMeshAgent))]
public class MoveAgent : MonoBehaviour
{
    //�ν����� �信 ǥ���� �ִϸ��̼� Ŭ���� ����
    public EnemyAnim enemyAnim;
    public Animation anim; //Animation ������Ʈ ����

    //���� ������ �����ϱ� ���� ����Ʈ Ÿ�� ����
    //����Ʈ�� �迭�� ������ �ڷᱸ��
    //�迭�� ���̴� ���������̴�.
    //�����Ͱ� ������ ���̰� �þ��, �����Ǹ� �پ��
    public List<Transform> wayPoints;//Generic �Ҽ��̴�.
    public int nextIdx; //���� ���� ������ �迭 Index

    NavMeshAgent agent;

    readonly float patrolSpeed = 1.5f;//�����ӵ�
    readonly float traceSpeed = 4f;//�����ӵ�



    float damping = 1f;//ȸ���� �ӵ� ���� ���
    Transform playerTr;
    Transform enemyTr;
    float nextAttack = 0f;
    readonly float AttackRate = 0.1f;
    

    public bool isAttack = false; //�߻� ���� �Ǵܺ���
    public AudioClip AttackSfx;




    private bool _patrolling;
    //������Ƽ = �޼ҵ��� �����̳� ����ó�� �����ϴ� ��
    //�������� �⺻���� ���Ȱ� ������ ��/�� ��Ȳ�� ��Ʈ���� �� ���
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
                //��������� �� ȸ�����
                damping = 1f; //��������϶��� �� �����ϰ�
                MoveWayPoint();
            }
        }
    }

    Vector3 _traceTarget;
    public Vector3 TRACETARGET //������Ƽ
    {
        get
        {
            return _traceTarget;
        }
        set
        {
            _traceTarget = value;
            agent.speed = traceSpeed;
            damping = 7f;//Trace����϶� ������庸�� 7�� ������ ���� ȸ�� ��ų �� �ִ�.

            //���� ��� �����ϴ� �Լ� ȣ��
            TraceTarget(_traceTarget);
        }
    }

    //�̵��ӵ��� ���� ������Ƽ
    public float SPEED
    {
        //get�� �����Ͽ� ���� ������ ���� ����
        //������ͼ� ��븸 ����
        get { return agent.velocity.magnitude; }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerTr = GameObject.FindGameObjectWithTag("PLAYER").GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        //�������� ������ ���� �ӵ��� ���̴� ����� ����.
        //�����ӿ��� Enemy�� �÷��̾ �����ϹǷ�
        //�ӵ��� ������ �ʵ��� �Ѵ�.
        agent.autoBraking = false;
        agent.speed = patrolSpeed;

        enemyTr = GetComponent<Transform>();
        //NavMeshAgent�� ȸ���� �ڵ� �����ϴ� �κ� ����
        agent.updateRotation = false;
        anim = GetComponent<Animation>();
        anim.clip = enemyAnim.idle; //���� ���۽� ����� Ŭ������
        anim.Play();



        //���̾��Ű���� WayPointGroup�̶�� �̸��� ������ ������Ʈ�� �˻��Ͽ� ����
        //Finde �Լ��� ������ ������ ������ ������ ������Ʈ �Լ������� FInd�� ȣ���ϴ� ���� ���ذ���
        var group = GameObject.Find("WayPointGroup");
        //null üũ�� ���ؼ� �ش� ������Ʈ�� �����ϴ��� üũ
        if (group != null)
        {   //������ WayPointGroup ��� �̸��� ������Ʈ����
            //�ڽ� ������Ʈ�� Tranfrom �� ��� ������
            //���Ŀ� wayPoints��� ����Ʈ�� �߰����� -> ������ ���� ����Ʈ �迭
            //GetComponentsInChildern �޼ҵ�
            //�ڱ� �ڽ��� �����Ͽ� ������ �����ϴ� ��� ������Ʈ�� ������
            group.GetComponentsInChildren<Transform>(wayPoints); //�ڽĿ�����Ʈ�� �� �� ���ܿ�
            //����Ʈ���� 0��° �ε����� �ش��ϴ� ��� ����
            wayPoints.RemoveAt(0);//�θ� ������Ʈ�� ���� ���ԵǱ� ������ 0���� ������Ʈ�� ���������ش�.

            //���� ���������� ������ ��ġ�� ����
            nextIdx = Random.Range(0, wayPoints.Count);
        }

        //������ �̵���Ű�� �Լ� ȣ��

        //MoveWayPoint();
        
        PATROLLING = true;

    }

    void MoveWayPoint()
    {
        //isPathStale ��� ��� ������ ��ȯ�ϴ� �Լ�
        //��� ������̶�� ������ �̵��� ���� ����
        //��� ����� ������ �� ������ �̵��ϵ��� ��
        if (agent.isPathStale)//bool �ڷ��� 0,1 �� ��ȯ�ϴ� �ڷ����̴�. �� ������ϋ��� ���̱⶧���� �����ϰ� ����� ������ �Ʒ� �ڵ带 �����Ѵ�.
            return;
        //����� ���� �Ŀ� ���� ��ġ�� �̵�
        agent.destination = wayPoints[nextIdx].position;
        //���� �̵� ����
        agent.isStopped = false;
        anim.CrossFade(enemyAnim.Walk.name, 0.3f);
    }

    void TraceTarget(Vector3 pos)
    {
        //��� ������϶��� �н�
        if (agent.isPathStale)
            return;

        agent.destination = pos;
        agent.isStopped = false;
        anim.CrossFade(enemyAnim.Run.name, 0.3f);

    }
    public void Stop()
    {
        //������Ʈ ������ ���߱�
        agent.isStopped = true;
        //Ȥ�ó� �������� �𸣱� ������ �����ӵ� 0���� ����
        agent.velocity = Vector3.zero;
        _patrolling = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        //���� �̵����϶� �����
        if (agent.isStopped == false)
        {
            //NavMeshAgent �� �����ؾߵ� ������ ����
            Quaternion rot = Quaternion.LookRotation(agent.desiredVelocity); //desiredVelocity ���� ���ϴ� �������� �� ư��

            enemyTr.rotation = Quaternion.Slerp(enemyTr.rotation, rot, Time.deltaTime * damping);//ī�޶� ���� �����
        }


        //���� ��尡 �ƴ� ��쿡 �Ʒ� �ڵ� �������
        //���� ����� ���� ���� ���� ��������ϴ� �ڵ� ����
        if (!_patrolling)
            //return;
        //agent.velocity.magnitude = NavMesh�� �ӵ� //10�� ������ ���������� ��Ʈ 10�� ������ �Ѵ�.
        //agent.velocity.sqrMagnitude�� ������ ������ �̸����༭ ������ ���� �����.
        //�ش������� ���� �����̴� ���ε� �������� �ٿͰ��� ��Ȳ�� �߱���
        if (agent.velocity.sqrMagnitude >= 0.2f * 0.2f && agent.remainingDistance <= 0.5f)
        {

            nextIdx++;
            //nextIdx ���� �����̼� ��Ű�� ���ؼ� ���
            //ex) 0%3 = 0, 1%3 = 1 ...3%3 = 0
            //���� ���� �ڿ� �����������ϴ� ������ŭ ������ �ٽ� 0���� ���ƿ��鼭 �����̼� ���Եȴ�.
            nextIdx = nextIdx % wayPoints.Count;

            nextIdx = Random.Range(0, wayPoints.Count);
            MoveWayPoint();
        }
        //�������� �ƴϸ鼭 ���� ������ ����
        if (isAttack == true )
        {
            //Time.time ������ ����� �� ��� �ð�

            //���� �߻�ð� = ����ð� + �߻簣�� + ������ 0 ~ 0.3��
           
          
           
            //�����Լ� ȣ��
            Attack();

            nextAttack = Time.time + AttackRate + Random.Range(0f, 0.3f);
           
            //                                               A                  B
            Quaternion rot = Quaternion.LookRotation(playerTr.position - enemyTr.position);

            //������ ���� = A ���� - B ���� = B���� A�� �ִ� ��ġ�� ����Ŵ
            //��, enemyTr���� playerTr�� ����Ű�� ����
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