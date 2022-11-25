using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageScoreCnt : MonoBehaviour
{
    private StageScoreCnt stageScoreCnt;

    public GameObject GameClearUI; // ���� Ŭ���� UI
    public Text scoreText;

    public int maxScoreCnt;
    public int curScoreCnt = 0;

    private bool getAllScore = false; // ��� ���� ȹ�� �� true

    public int MaxScoreCnt => maxScoreCnt;

    void Awake()
    {
        //GameClearUI.SetActive(false);
        //scoreText = GetComponent<Text>();

        //// SCORE �±׸� ���� ������Ʈ ������ maxScoreCnt�� ����
        //maxScoreCnt = GameObject.FindGameObjectsWithTag("SCORE").Length;
        //curScoreCnt = maxScoreCnt;
    }

    void Update()
    {
        //scoreText.text = "SCORE : " + stageScoreCnt.curScoreCnt + "/" + stageScoreCnt.maxScoreCnt;

        //if (getAllScore == true)
        //{
        //    // ��� ������ ȹ���ϰ� Enter�� ������
        //    if (Input.GetKeyDown(KeyCode.Return))
        //    {
        //        // ���� �޴� or �ٸ� ���� Ŭ���� UI ����
        //    }
        //}
    }

    public void GetScore()
    {
        curScoreCnt++;
        Debug.Log("CurScore : " + curScoreCnt);

        if (curScoreCnt == maxScoreCnt)
        {
            // ���� Ŭ����
            getAllScore = true;
            Debug.Log("Game Clear");

            // �Ͻ�����
            Time.timeScale = 0.0f;

            GameClearUI.SetActive(true);
        }
    }
}
