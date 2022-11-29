using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public enum ERoom { kHWRoom, kWJRoom1, kSMRoom, kHWRoom1 }

    public GameObject HWRoom;
    public GameObject WJRoom1;
    public GameObject SMRoom;
    public GameObject HWRoom1;
    
    

    public bool open = false;

    private void Start()
    {
        HWRoom = Instantiate(HWRoom, transform.position, Quaternion.identity);
        WJRoom1 = Instantiate(WJRoom1, transform.position, Quaternion.identity);
        SMRoom = Instantiate(SMRoom, transform.position, Quaternion.identity);
        HWRoom1 = Instantiate(HWRoom1, transform.position, Quaternion.identity);

        HWRoom.SetActive(false);
        WJRoom1.SetActive(false);
        SMRoom.SetActive(false);
        HWRoom1.SetActive(false);

    }

    //private void Update()
    //{
    //    if (open)
    //    {
    //        HWRoom.SetActive(true);
    //    }
    //    else
    //    {
    //        HWRoom.SetActive(false);
    //    }
    //}

    public void LoadRoom(ERoom _room, bool _isOpen)
    {
        switch (_room)
        {
            case ERoom.kHWRoom:
                HWRoom.SetActive(_isOpen);

                break;
            case ERoom.kWJRoom1:
                WJRoom1.SetActive(_isOpen);
                break;
            case ERoom.kSMRoom:
                SMRoom.SetActive(_isOpen);
                break;
            case ERoom.kHWRoom1:
                HWRoom1.SetActive(_isOpen);
                break;

        }

      
    }

}
