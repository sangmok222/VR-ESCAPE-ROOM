using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class EnemyAttack : MonoBehaviour
{

    //AudioSource audio;
    //Animator animator;
    Transform playerTr;
    Transform enemyTr;
    //readonly int hashFire = Animator.StringToHash("Fire");
    //readonly int hashReload = Animator.StringToHash("Reload");

    //발사 관련 변수
    float nextAttack = 0f;
    readonly float AttackRate = 0.1f;
    readonly float damping = 10f;

    public bool isAttack = false; //발사 유무 판단변수
    public AudioClip AttackSfx;

    //재장전 관련 변수
    //readonly float reloadTime = 2f;
    //readonly int maxBullet = 10;
    //int currBullet = 10;
    //bool isReload = false;
    WaitForSeconds wsReload;




    // Start is called before the first frame update
    void Start()
    {
        playerTr = GameObject.FindGameObjectWithTag("PLAYER").GetComponent<Transform>();

        //animator = GetComponent<Animator>();
        // audio = GetComponent<AudioSource>();

        //wsReload = new WaitForSeconds(reloadTime);



    }


    void Update()
    {
        //재장전이 아니면서 공격 가능할 때만
        if (isAttack)
        {
            //Time.time 게임이 실행된 후 경과 시간

            //다음 발사시간 = 현재시간 + 발사간격 + 랜덤한 0 ~ 0.3초
            if (Time.time >= nextAttack)
            {
                //공격함수 호츨
                //Attack();

                nextAttack = Time.time + AttackRate + Random.Range(0f, 0.3f);
            }
            //                                               A                  B
            Quaternion rot = Quaternion.LookRotation(playerTr.position - enemyTr.position);

            //벡터의 빼기 = A 벡터 - B 벡터 = B에서 A가 있는 위치를 가르킴
            //즉, enemyTr에서 playerTr을 가르키는 벡터
            enemyTr.rotation = Quaternion.Slerp(enemyTr.rotation, rot, Time.deltaTime * damping);
        }
    }





}
