using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickLeft))
        {
            Vector3 eulerAngle = new Vector3(0f, 2f, 0f); // 예시

            transform.Rotate(eulerAngle, Space.Self);

            // Space.Self일 때의 Rotate() 내부 구현
            transform.localRotation *= Quaternion.Euler(eulerAngle);

        }
    }
}
