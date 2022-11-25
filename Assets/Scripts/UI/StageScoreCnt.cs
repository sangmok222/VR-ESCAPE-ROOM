using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageScoreCnt : MonoBehaviour
{
    private StageScoreCnt stageScoreCnt;

    public GameObject GameClearUI; // 게임 클리어 UI
    public Text scoreText;

    public int maxScoreCnt;
    public int curScoreCnt = 0;

    private bool getAllScore = false; // 모든 점수 획득 시 true

    public int MaxScoreCnt => maxScoreCnt;

    void Awake()
    {
        //GameClearUI.SetActive(false);
        //scoreText = GetComponent<Text>();

        //// SCORE 태그를 지닌 오브젝트 개수를 maxScoreCnt에 저장
        //maxScoreCnt = GameObject.FindGameObjectsWithTag("SCORE").Length;
        //curScoreCnt = maxScoreCnt;
    }

    void Update()
    {
        //scoreText.text = "SCORE : " + stageScoreCnt.curScoreCnt + "/" + stageScoreCnt.maxScoreCnt;

        //if (getAllScore == true)
        //{
        //    // 모든 코인을 획득하고 Enter를 누르면
        //    if (Input.GetKeyDown(KeyCode.Return))
        //    {
        //        // 메인 메뉴 or 다른 게임 클리어 UI 띄우기
        //    }
        //}
    }

    public void GetScore()
    {
        curScoreCnt++;
        Debug.Log("CurScore : " + curScoreCnt);

        if (curScoreCnt == maxScoreCnt)
        {
            // 게임 클리어
            getAllScore = true;
            Debug.Log("Game Clear");

            // 일시정지
            Time.timeScale = 0.0f;

            GameClearUI.SetActive(true);
        }
    }
}
