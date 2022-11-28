using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    /*
    public GameObject D1;
    public GameObject D2;
    public GameObject D3;
    public GameObject D4;
    public GameObject D5;
    public GameObject D6;
    public GameObject D7;
    public GameObject D8;
    public GameObject Door_V2;
    */

    PassWord password;

    public float interactDiastance = 5f;
    private Animator ani;
    bool open;
    private void Start()
    {
        password = GameObject.Find("Keypad").GetComponent<PassWord>();
        ani = GetComponent<Animator>();
    }
    void Update()
    {
        //Ray ray = new Ray(transform.position, transform.forward);
        //RaycastHit hit;
        //Debug.DrawRay(transform.position, hit.point, Color.green);
        // 하나의 태그로관리 2022.11.24 우진
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    Ray ray = new Ray(transform.position, transform.forward);
        //    RaycastHit hit;
        //    if (Physics.Raycast(ray, out hit, interactDiastance))
        //    {
        //        if (hit.collider.CompareTag("Door"))
        //        {
        //            GameObject.Find("Door_V1").GetComponent<Door>().ChangeDoorState();

        //        }
        //    }
        //}
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    Ray ray = new Ray(transform.position, transform.forward);
        //    RaycastHit hit;
        //    if (Physics.Raycast(ray, out hit, interactDiastance))
        //    {
        //        if (hit.collider.CompareTag("Door1"))
        //        {
        //            GameObject.Find("Door_V1 (1)").GetComponent<Door>().ChangeDoorState();

        //        }
        //    }
        //}//
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    Ray ray = new Ray(transform.position, transform.forward);
        //    RaycastHit hit;
        //    if (Physics.Raycast(ray, out hit, interactDiastance))
        //    {
        //        if (hit.collider.CompareTag("Door2"))
        //        {
        //            GameObject.Find("Door_V1 (2)").GetComponent<Door>().ChangeDoorState();

        //        }
        //    }
        //}//
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    Ray ray = new Ray(transform.position, transform.forward);
        //    RaycastHit hit;
        //    if (Physics.Raycast(ray, out hit, interactDiastance))
        //    {
        //        if (hit.collider.CompareTag("Door3"))
        //        {
        //            GameObject.Find("Door_V1 (5)").GetComponent<Door>().ChangeDoorState();

        //        }
        //    }
        //}//
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    Ray ray = new Ray(transform.position, transform.forward);
        //    RaycastHit hit;
        //    if (Physics.Raycast(ray, out hit, interactDiastance))
        //    {
        //        if (hit.collider.CompareTag("Door4"))
        //        {
        //            GameObject.Find("Door_V1 (3)").GetComponent<Door>().ChangeDoorState();

        //        }
        //    }
        //}//
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    Ray ray = new Ray(transform.position, transform.forward);
        //    RaycastHit hit;
        //    if (Physics.Raycast(ray, out hit, interactDiastance))
        //    {
        //        if (hit.collider.CompareTag("Door5"))
        //        {
        //            GameObject.Find("Door_V1 (4)").GetComponent<Door>().ChangeDoorState();

        //        }
        //    }
        //}//
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    Ray ray = new Ray(transform.position, transform.forward);
        //    RaycastHit hit;
        //    if (Physics.Raycast(ray, out hit, interactDiastance))
        //    {
        //        if (hit.collider.CompareTag("Door7"))
        //        {
        //            GameObject.Find("Door_V1 (7)").GetComponent<Door>().ChangeDoorState();

        //        }
        //    }
        //}//


        //if (OVRInput.GetDown(OVRInput.Button.Two) || Input.GetKeyDown(KeyCode.E))
        //{
        //    Ray ray = new Ray(transform.position, transform.forward);
        //    RaycastHit hit;
        //    if (Physics.Raycast(ray, out hit, interactDiastance))
        //    {
        //        if (hit.collider.CompareTag("Door6"))
        //        {
        //            GameObject.Find("Door_V1 (6)").GetComponent<Door>().ChangeDoorState();

        //        }
        //    }
        //}
        if (OVRInput.GetDown(OVRInput.Button.Two) || Input.GetKeyDown(KeyCode.E))
        {
            
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, interactDiastance))
            {
                Debug.DrawRay(transform.position, hit.point, Color.green);
                //if (hit.collider.CompareTag("FDOOR"))
                //{
                //    hit.transform.GetComponent<FridgeOpen>().ChangeDoorState();
                //}
                //else if (hit.collider.CompareTag("FDOOR"))
                //{
                //    GameObject.Find("FridgeDoor").GetComponent<FridgeOpen>().ChangeDoorState();
                //}

                //else if (hit.collider.CompareTag("SHEET"))
                //{
                //    hit.transform.GetComponent<SheetRackCaseAni>().aniplay();
                }
                //if (hit.collider.CompareTag("Door"))
                //{
                //    hit.transform.GetComponent<Door>().ChangeDoorState();
                //}
                if (hit.collider.CompareTag("NUM"))
                {
                    
                    password.num = hit.transform.gameObject.name;
                    password.GetComponent<PassWord>().Click();
                }
            }
        }//

    }
