using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorResistance : MonoBehaviour
{
    [SerializeField] private Animator doorLeft = null;
    [SerializeField] private Animator doorRight = null;

    Character character;
    Character boy;
    Character girl;

    public void Start()
    {
        boy = GameObject.Find("BoyCharacter").GetComponent<Character>();
        girl = GameObject.Find("GirlCharacter").GetComponent<Character>();
    }
    public void OpenDoor(Animator door)
    {
        door.SetBool("idOpenRight", true);
        door.SetBool("isOpen", true);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            character = collider.GetComponent<Character>();
            if (girl.GetHasMap() && boy.GetHasMapBoy())
            {
                OpenDoor(doorLeft);
                OpenDoor(doorRight);
            }
            else
            {
                character.DisplayMessage("You haven't solved all challenges.");
            }
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        character = collider.GetComponent<Character>();
        character.DisplayMessage("");
    }

}
