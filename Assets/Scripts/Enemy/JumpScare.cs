using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScare : MonoBehaviour
{
    float delayTime;

    public GameObject horrorHand;
    public GameObject zombie;
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "PLAYER")
        {
            delayTime += Time.deltaTime;
            if(delayTime >= 0.5f)
            horrorHand.SetActive(true);

            if (delayTime >= 2.6f)
            {
            zombie.SetActive(false);
            horrorHand.SetActive(false);
            //delayTime = 0;
            }

        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        delayTime = 0f;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        
        Debug.Log(delayTime);
    }
}
