using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Gloves : MonoBehaviour
{
    [SerializeField]
    public static bool in_Gloves = false;

    // private ������ �ν����Ϳ� ǥ��
    [SerializeField]
    private GameObject attachGo = null;
    // ���ϴ� ������Ʈ������ ���� ������,
    // �ش� ������Ʈ�� �ٷ� ������ �� ����
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
            // �θ�-�ڽ� ���� �����(��������)
            attachGo.transform.SetParent(handTr);
            attachGo.transform.localPosition = Vector3.zero;
        }

        //if (Input.GetKeyDown(KeyCode.G))
        if (OVRInput.GetDown(OVRInput.Button.Four))
        {
            // �и�
            //attachGo.transform.parent = null;
            hand_Gloves.SetActive(false);
        }

    }


}