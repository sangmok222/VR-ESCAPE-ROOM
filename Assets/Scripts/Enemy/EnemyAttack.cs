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

    //�߻� ���� ����
    float nextAttack = 0f;
    readonly float AttackRate = 0.1f;
    readonly float damping = 10f;

    public bool isAttack = false; //�߻� ���� �Ǵܺ���
    public AudioClip AttackSfx;

    //������ ���� ����
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
        //�������� �ƴϸ鼭 ���� ������ ����
        if (isAttack)
        {
            //Time.time ������ ����� �� ��� �ð�

            //���� �߻�ð� = ����ð� + �߻簣�� + ������ 0 ~ 0.3��
            if (Time.time >= nextAttack)
            {
                //�����Լ� ȣ��
                //Attack();

                nextAttack = Time.time + AttackRate + Random.Range(0f, 0.3f);
            }
            //                                               A                  B
            Quaternion rot = Quaternion.LookRotation(playerTr.position - enemyTr.position);

            //������ ���� = A ���� - B ���� = B���� A�� �ִ� ��ġ�� ����Ŵ
            //��, enemyTr���� playerTr�� ����Ű�� ����
            enemyTr.rotation = Quaternion.Slerp(enemyTr.rotation, rot, Time.deltaTime * damping);
        }
    }





}
