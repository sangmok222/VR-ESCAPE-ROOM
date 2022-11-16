using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    public GameObject subUI;
    public GameObject main;
    public GameObject gameUI;

    private bool subUIisAction;

    void Start()
    {
        Time.timeScale = 0.0f;
        main.SetActive(true);
        gameUI.SetActive(true);
        subUI.SetActive(false);

    }

    void Update()
    {
        SubUITrigger();
    }

    public void GameQuit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    public void PauseGame()
    {
        subUI.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void PlayGame()
    {
        subUI.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void SubUITrigger()
    {
        if (Input.GetButtonDown("Escape"))
        {
            if (subUI.activeSelf)
            {
                PlayGame();
            }
        }
        else
        {
            PauseGame();
        }
    }

    public void CloseSubUI()
    {

    }

}
