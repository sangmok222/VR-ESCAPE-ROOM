using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public static bool isWater = false;

    [SerializeField] private float waterDrag; // �� �� �߷�
    private float originDrag; // �� �� ������ ���� ���׷�

    [SerializeField] private Color waterColor; // ���� �� �� Fog ����
    [SerializeField] private float waterFogDensity; // ���� �� �� Ź�� ����

    private Color originColor;
    private float originFogDensity;


    void Start()
    {
        originColor = RenderSettings.fogColor;
        originFogDensity = RenderSettings.fogDensity;

        originDrag = 0;
    }
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "PLAYER")
        {
            GetInWater(other);  // ���� ��
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "PLAYER")
        { 
            GetOutWater(other);  // ������ ����
        }
    }



    private void GetInWater(Collider _player)
    {
        isWater = true;
        _player.transform.GetComponent<Rigidbody>().drag = waterDrag;// �߷����� ==> õõ�� �������

        RenderSettings.fogColor = waterColor;
        RenderSettings.fogDensity = waterFogDensity;

    }

    private void GetOutWater(Collider _player)
    {
        if(isWater)
        {
            isWater = false;
            _player.transform.GetComponent<Rigidbody>().drag = originDrag;

            RenderSettings.fogColor = originColor;
            RenderSettings.fogDensity = originFogDensity;

        }
    }
}