using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCntP : MonoBehaviour
{
    // SCORE 태그를 붙인 오브젝트에 스트립트 활성화

    [SerializeField]
    private StageScoreCnt stageScoreCnt;
    private GameObject scorePrefab;

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        // 점수를 환산할 함수 호출
        stageScoreCnt.GetScore();

        Destroy(gameObject);
    }

}
