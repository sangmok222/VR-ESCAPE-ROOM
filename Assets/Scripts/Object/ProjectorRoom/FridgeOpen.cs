using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeOpen : MonoBehaviour
{
    float rotSpeed = 50f;

    private bool isRotate = false;

    void Update()
    {
       if(Input.GetKeyDown(KeyCode.O))
        {
            if (Quaternion.Angle(transform.rotation, Quaternion.Euler(0, 0, 0)) < -90f)
            {
                transform.rotation = Quaternion.Euler(0, -90, 0);
                isRotate = true;
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                isRotate = false;
            }
            
           
        }
    }

}
