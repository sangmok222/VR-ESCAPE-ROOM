using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("�� ������ ����")]
    public Transform[] points; //�� ĳ���� ���� ��ġ
    public GameObject enemy;
    public float createTime = 2f;//���� �ֱ�
    public int maxEnemy = 10;

    public bool isGameOver = false;
    //�̱��Ͽ� �����ϱ� ���� static ���� 
    //static�� ���� è�Ǿ� ������ �ʾƵ� ��ΰ� �� �� �ִ� ����
    public static GameManager instance;

   


   

    private void Awake()
    {
        if (instance == null)
        {
            //�ش� ���� �Ŵ���(�ڱ��ڽ�)�� �Ҵ�
            instance = this;
        }
        //instance�� �Ҵ�� Ŭ������ �ڱ� �ڽ��� �ƴ϶�� ���� ������ Ŭ������ �ǹ��� �ᱹ Ŭ���� üũ�ϴ�������
        //����ũ�ϰ� �ش� ���ӿ� GameManger�� �ϳ��� �����ϵ��� �Ϸ���
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
        //�� ������ �Ͼ���� �������� �ʰ� ��� ����
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        points = GameObject.Find("SpawnPointGroup").GetComponentsInChildren<Transform>();
        //points�� ������ ��쿡
        if (points.Length > 0)
        {
            //Enemy �ڵ� ���� �ڷ�ƾ �Լ� ȣ��
            StartCoroutine(CreatEnemy());
        }
        DontDestroyOnLoad(gameObject);
        
    }

    IEnumerator CreatEnemy()
    {
        //���� ���� ������ ���ѷ���
        while (!isGameOver)
        {
            //ENEMY �±׸� ���� ������Ʈ�� ���� �ľ�
            //�ִ� 10������ �ѱ��� �ʱ� ���ؼ�
            int enemyCount = GameObject.FindGameObjectsWithTag("ENEMY").Length;
            //Debug.Log(enemyCount);
            if (enemyCount < maxEnemy)
            {
                yield return new WaitForSeconds(createTime);

                int idx = Random.Range(1, points.Length);
                Instantiate(enemy,//Enemy, ������
                            points[idx].position,//��������� ��ġ
                            points[idx].rotation);//��������� ��ġ�� ȸ����
            }
            else
                yield return null;
        }
    }

    
    
    //��ȯ���� GameObject�� ������ ������Ʈ Ǯ���� ��Ȱ��ȭ �Ǿ��ִ� ������Ʈ�� return �ϱ� ����
   



}