using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hpbar : MonoBehaviour
{
    [SerializeField]
    private Slider hpBar;

    public float maxHp;
    private float curHp;
    public float damage;
    private float imsi;

    private Color curColor;
    private readonly Color initColor = new Vector4(0, 1f, 0f, 1f);


    void Awake()
    {
        curHp = maxHp;
        hpBar.value = (float)curHp / (float)maxHp;

        // hpBar.color = initColor;
        curColor = initColor;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            curHp -= damage;
            Debug.Log("CurHp : " + curHp);
        }
        imsi = (float)curHp / (float)maxHp;
        DamagedHp();
    }

    void DamagedHp()
    {
        hpBar.value = Mathf.Lerp(hpBar.value, imsi, Time.deltaTime * damage);
    }

    void DisplayHpbar()
    {
        if (imsi > 0.5f)
        {
            curColor.r = (1 - imsi) * 2f;
        }
        else
        {
            curColor.g = imsi * 2f;
        }

        // hpBar.color = curColor;
        // hpBar.fillAmount = imsi;
    }
}
