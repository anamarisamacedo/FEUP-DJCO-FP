using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorResistance : MonoBehaviour
{
    [SerializeField] private Animator doorLeft = null;
    [SerializeField] private Animator doorRight = null;

    Character character;

    public void OpenDoor(Animator door)
    {
        door.SetBool("isOpen", true);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            character = collider.GetComponent<Character>();
            OpenDoor(doorLeft);
            OpenDoor(doorRight);
        }
    }

}
