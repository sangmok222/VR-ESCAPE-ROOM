using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Broken : MonoBehaviour
{
    public GameObject prefab;
    GameObject kettle;
    void Start()
    {
        //prefab = Resources.Load<GameObject>("Kettles");
        //kettle = Instantiate(prefab);

        //Destroy(kettle, 3.0f);
    }

    void Update()
    {
        
    }

    void KettleBorken()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ASYLUM") //������ �� �±׿� �ε����� ���� �÷��̾ �ֵθ��� ���⳪ ��� ���⿡ �¾Ƶ� ȿ���� �������� || Ȱ���� ��
        {
            Destroy(this.gameObject); //�ڱ� ������Ʈ ����
            GameObject go = Instantiate(prefab, transform.position, Quaternion.identity);
            //prefab = Resources.Load<GameObject>("Kettles");
            //kettle = Instantiate(prefab,transform.position, Quaternion.identity, null);
            //Destroy(go, 3.0f); //����� ������ 3���� ����
        }
    }
}
