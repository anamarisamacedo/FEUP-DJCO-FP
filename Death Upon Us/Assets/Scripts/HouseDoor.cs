using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseDoor : MonoBehaviour
{
    [SerializeField] private Animator myDoor = null;


    public void OpenDoor()
    {
        myDoor.SetBool("isOpen", true);
    }

    public void CloseDoor()
    {
        myDoor.SetBool("isOpen", false);
    }
}