using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScare : MonoBehaviour
{
    float delayTime;

    public GameObject horrorHand;
    public GameObject zombie;
    public GameObject point;
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "PLAYER")
        {
            delayTime += Time.deltaTime;
            if(delayTime >=  1f)
            {
                zombie.GetComponent<Transform>().localPosition = point.transform.localPosition;
                horrorHand.SetActive(true);
            }
            

            if ( 1f< delayTime && delayTime >= 2.6f)
            {
            zombie.SetActive(false);
            horrorHand.SetActive(false);
            //delayTime = 0;
            }
            if( delayTime >=2.7f)
                Destroy(gameObject);

        }
        
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

       
        
    }
}
