using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public GameObject D1;
    public GameObject D2;
    public GameObject D3;
    public GameObject D4;
    public GameObject D5;
    public GameObject D6;
    public GameObject D7;
    public GameObject D8;
    public GameObject Door_V2;
    
    
    
    public float interactDiastance = 5f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, interactDiastance))
            {
                if (hit.collider.CompareTag("Door"))
                {
                    GameObject.Find("Door_V1").GetComponent<Door>().ChangeDoorState();
                    
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, interactDiastance))
            {
                if (hit.collider.CompareTag("Door1"))
                {
                    GameObject.Find("Door_V1 (1)").GetComponent<Door>().ChangeDoorState();

                }
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, interactDiastance))
            {
                if (hit.collider.CompareTag("Door2"))
                {
                    GameObject.Find("Door_V1 (2)").GetComponent<Door>().ChangeDoorState();

                }
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, interactDiastance))
            {
                if (hit.collider.CompareTag("Door3"))
                {
                    GameObject.Find("Door_V1 (3)").GetComponent<Door>().ChangeDoorState();

                }
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, interactDiastance))
            {
                if (hit.collider.CompareTag("Door4"))
                {
                    GameObject.Find("Door_V1 (4)").GetComponent<Door>().ChangeDoorState();

                }
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, interactDiastance))
            {
                if (hit.collider.CompareTag("Door5"))
                {
                    GameObject.Find("Door_V1 (5)").GetComponent<Door>().ChangeDoorState();

                }
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, interactDiastance))
            {
                if (hit.collider.CompareTag("Door6"))
                {
                    GameObject.Find("Door_V1 (6)").GetComponent<Door>().ChangeDoorState();

                }
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, interactDiastance))
            {
                if (hit.collider.CompareTag("Door7"))
                {
                    GameObject.Find("Door_V1 (7)").GetComponent<Door>().ChangeDoorState();

                }
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, interactDiastance))
            {
                if (hit.collider.CompareTag("Door8"))
                {
                    GameObject.Find("Door_V1 (8)").GetComponent<Door>().ChangeDoorState();

                }
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, interactDiastance))
            {
                if (hit.collider.CompareTag("Door8"))
                {
                    GameObject.Find("Door_V1 (8)").GetComponent<Door>().ChangeDoorState();

                }
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, interactDiastance))
            {
                if (hit.collider.CompareTag("Door_V2"))
                {
                    GameObject.Find("Door_V2").GetComponent<Door>().ChangeDoorState();

                }
            }
        }

    }
}
