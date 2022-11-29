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
    
    
    public float interactDiastance = 6f;
    private Animator ani;
    private RoomManager rm;
    
    private void Start()
    {
        ani = GetComponent<Animator>();
        rm = GameObject.Find("RoomManager").GetComponent<RoomManager>();
    }
    void Update()
    {
        
        if (OVRInput.GetDown(OVRInput.Button.Two) || Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, interactDiastance))
            {
                if (hit.collider.CompareTag("FDOOR"))
                {
                    hit.transform.GetComponent<FridgeOpen>().ChangeDoorState();
                }
                //else if (hit.collider.CompareTag("FDOOR"))
                //{
                //    GameObject.Find("FridgeDoor").GetComponent<FridgeOpen>().ChangeDoorState();
                //}
                
                else if (hit.collider.CompareTag("SHEET"))
                {
                    hit.transform.GetComponent<SheetRackCaseAni>().aniplay();
                }   
                else if (hit.collider.CompareTag("Door"))
                {
                    hit.transform.GetComponent<Door>().ChangeDoorState();
                    rm.LoadRoom(hit.transform.GetComponent<Door>().roomType, true);
                }
                
            }
        }
       
    }
}
