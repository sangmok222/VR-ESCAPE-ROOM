using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFOV : MonoBehaviour
{
    [Range(0, 360)]
    public float viewAngle = 120f;//�þ߰�
    public float viewRange = 15f;//�þ߹���

    [Header("�÷��̾� �Ǵܿ� �ʿ��� ����")]
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
        //���̾� ��ø
        //������ ������ �÷��̾� ���̾�� ����Ŭ ���̾� �ΰ��� OR �����Ͽ� ������ ���̾ ����
        //������ Barrel �� ����ߵ� ���̾� ������ ���� ���� ������ ���� �� �� ����.
        layerMask = 1 << playerLayer | 1 << obstacleLayer;
    }

    //�� ���� �ִ� ���� ��ǥ���� ����ϴ� �޼ҵ�
    public Vector3 CirclePoint(float angle)
    {
        //Ʈ�������� ���Ϸ� ������ ������ �°��� ���� ��ǥ�� �������� �����ϱ� ���Ͽ� ��� Enemy�� y ȸ������ ����
        angle += transform.eulerAngles.y;
        //�� ���� ��ǥ�� �������� ���Ͽ� �ﰢ�Լ� Ȱ��
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));
    }
    //���� ������ �Ǵ��ϱ� ���� �޼ҵ�
    public bool isTracePlayer()
    {
        bool isTrace = false;
        Collider[] colls = Physics.OverlapSphere(enemyTr.position, viewRange, 1 << playerLayer);
        //�÷��̾� ���̾ �����ϹǷ� 1�� ��� = �÷��̾ ������ ��� ���� ��
        if (colls.Length == 1)
        {                   // A��ġ - B��ġ = B���� A�� ����Ű�� ����
            Vector3 dir = (playerTr.position - enemyTr.position).normalized;

            //Enemy �þ߰��� �÷��̾ ��ġ�ϴ��� �Ǵ�
            if (Vector3.Angle(enemyTr.forward, dir) < viewAngle * 0.5)
            {
                isTrace = true; //���� �Ѵٸ� ���� ����
            }
        }
        return isTrace;
    }

    //�÷��̾ ���������� ���� �ִ���
    //obtacle ���̾�� ������ ���°� �ƴ��� �Ǵ�
    public bool isViewPlayer()
    {
        bool isView = false;
        //����ĳ��Ʈ �Լ��� ��� �⺻ ��ȯ���� bool��
        //������ out Ű���带 ���ؼ� �浹 ������ ������ �̶� ���� ������ ������ ������ �̸� �����ؾ���
        RaycastHit hit;
        Vector3 dir = (playerTr.position - enemyTr.position).normalized;
        if (Physics.Raycast(enemyTr.position,   //�߻� ��ġ
                            dir,    //�߻� ����
                            out hit,    //�浹 ���� ����
                            viewRange,  //���� ����
                            layerMask))    //���� ���̾�
        {
            //����� ������Ʈ�� �÷��̾� �±׸� ������ ������ True �ƴϸ� false
            isView = hit.collider.CompareTag("PLAYER");
        }
        return isView;
    }



}
