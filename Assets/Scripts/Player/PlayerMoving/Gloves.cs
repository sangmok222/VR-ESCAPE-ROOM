using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Gloves : MonoBehaviour
{
    [SerializeField]
    public static bool in_Gloves = false;

    // private 변수를 인스펙터에 표시
    [SerializeField]
    private GameObject attachGo = null;
    // 원하는 컴포넌트명으로 전달 받으면,
    // 해당 컴포넌트를 바로 가져올 수 있음
    [SerializeField]
    private Transform handTr = null;

    [SerializeField]
    private GameObject hand_Gloves;


    void Start()
    {

    }


    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.T))
        if (OVRInput.GetDown(OVRInput.Button.Three))
        {
            // 부모-자식 관계 만들기(계층구조)
            attachGo.transform.SetParent(handTr);
            attachGo.transform.localPosition = Vector3.zero;
        }

        //if (Input.GetKeyDown(KeyCode.G))
        if (OVRInput.GetDown(OVRInput.Button.Four))
        {
            // 분리
            //attachGo.transform.parent = null;
            hand_Gloves.SetActive(false);
        }

    }


}