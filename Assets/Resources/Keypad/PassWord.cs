using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassWord : MonoBehaviour
{

    //카메라를 들고와서
    // private int[4] a;
    //private int[4] b;
    public GameObject passWordDisplay;
    public Text passWordText;
    //public GameObject[] passWordBtn;
    public string num;
    public string password;

    public void Click()
    {
        if (passWordText != null)
        {
            passWordText.text = password + num;
            password = passWordText.text;
        }
        else
        {
            passWordText.text = num;
            password = passWordText.text;
        }
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (passWordText != null)
            {
                passWordText.text = password + num;
                password = passWordText.text;
            }
            else
            {
                passWordText.text = num;
                password = passWordText.text;
            }
        }
        //passWordText.text = "****";
        //a[0] = 1;
        //a[1] = 2;
        //a[2] = 3;
        //a[3] = 4;

        //if (a[0] == b[0] && )
        //{//정답
        //    //문열림 함수
        //}
        //for(int i = 0; i< passWordBtn.Length; ++i)
        //{
        //    if (passWordBtn[i].name == "")
        //    {
        //        return i;
        //    }
        //}



    }
    //private int retrunValue()
    //{
    //    if (passWordBtn[0])
    //    {
    //       return 1;
    //    }
    //    else if (passWordBtn[1])
    //    {
    //        return 2;
    //    }
    //    else
    //    {
    //        return '*';
    //    }
    //}
    //private string hitObj()
    //{
    //    RaycastHit hit;
    //    return hit.transform.name;
    //}
    public int num1()
    {
        return 1;
    }
    public int num2()
    {
        return 2;
    }



}