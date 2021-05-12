using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseDoor : MonoBehaviour
{
    [SerializeField] private Animator myDoor = null;


    public void openDoor()
    {
        myDoor.SetBool("isOpen", true);
    }

    public void closeDoor()
    {
        myDoor.SetBool("isOpen", false);
    }
}