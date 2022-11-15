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
        if (collision.gameObject.tag == "ASYLUM") //지금은 맵 태그에 부딪히면 추후 플레이어가 휘두르는 무기나 쏘는 무기에 맞아도 효과가 있을려면 || 활용할 것
        {
            Destroy(this.gameObject); //자기 오브젝트 삭제
            GameObject go = Instantiate(prefab, transform.position, Quaternion.identity);
            //prefab = Resources.Load<GameObject>("Kettles");
            //kettle = Instantiate(prefab,transform.position, Quaternion.identity, null);
            //Destroy(go, 3.0f); //갖고온 프리펩 3초후 삭제
        }
    }
}
