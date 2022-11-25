using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("적 생성용 변수")]
    public Transform[] points; //적 캐릭터 스폰 위치
    public GameObject enemy;
    public float createTime = 2f;//생성 주기
    public int maxEnemy = 10;

    public bool isGameOver = false;
    //싱글턴에 접근하기 위한 static 변수 
    //static은 세계 챔피언 말하지 않아도 모두가 알 수 있는 변수
    public static GameManager instance;

   


   

    private void Awake()
    {
        if (instance == null)
        {
            //해당 게임 매니저(자기자신)을 할당
            instance = this;
        }
        //instance에 할당된 클래스가 자기 자신이 아니라면 새로 생성된 클래스를 의미함 결국 클래스 체크하는이유는
        //유니크하게 해당 게임에 GameManger가 하나만 존재하도록 하려고
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
        //씬 변경이 일어나더라도 삭제하지 않고 계속 유지
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        points = GameObject.Find("SpawnPointGroup").GetComponentsInChildren<Transform>();
        //points가 존재할 경우에
        if (points.Length > 0)
        {
            //Enemy 자동 생성 코루틴 함수 호출
            StartCoroutine(CreatEnemy());
        }
        DontDestroyOnLoad(gameObject);
        
    }

    IEnumerator CreatEnemy()
    {
        //게임 종료 전까지 무한루프
        while (!isGameOver)
        {
            //ENEMY 태그를 지닌 오브젝트의 갯수 파악
            //최대 10마리를 넘기지 않기 위해서
            int enemyCount = GameObject.FindGameObjectsWithTag("ENEMY").Length;
            //Debug.Log(enemyCount);
            if (enemyCount < maxEnemy)
            {
                yield return new WaitForSeconds(createTime);

                int idx = Random.Range(1, points.Length);
                Instantiate(enemy,//Enemy, 프리팹
                            points[idx].position,//랜덤추출된 위치
                            points[idx].rotation);//랜덤추출된 위치의 회전값
            }
            else
                yield return null;
        }
    }

    
    
    //반환형이 GameObject인 이유는 오브젝트 풀에서 비활성화 되어있는 오브젝트를 return 하기 때문
   



}