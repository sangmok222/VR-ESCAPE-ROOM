using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCntP : MonoBehaviour
{
    // SCORE �±׸� ���� ������Ʈ�� ��Ʈ��Ʈ Ȱ��ȭ

    [SerializeField]
    private StageScoreCnt stageScoreCnt;
    private GameObject scorePrefab;

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        // ������ ȯ���� �Լ� ȣ��
        stageScoreCnt.GetScore();

        Destroy(gameObject);
    }

}
