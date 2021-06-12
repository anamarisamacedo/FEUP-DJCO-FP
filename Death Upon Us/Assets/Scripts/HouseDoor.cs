using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class HouseDoor : MonoBehaviour
{
    [SerializeField] private Animator door1 = null;
    [SerializeField] private Animator door2 = null;
    [SerializeField] private GameObject? monsterOrange;
    [SerializeField] private GameObject? monsterPurple;
    [SerializeField] private GameObject? monsterBlue;
    bool hasKeysHouse1 = false;
    private Inventory inventory;

    public void OpenDoor(Animator door)
    {
        door.SetBool("isOpen", true);
    }

    public void CloseDoor(Animator door)
    {
        door.SetBool("isOpen", false);
    }

    private void OnTriggerEnter(Collider collider)
    {

        if (collider.CompareTag("Player"))
        {
            Character character = collider.GetComponent<Character>();
            if (this.CompareTag("House1")){
                hasKeysHouse1 = character.GetHasKeysHouse1();
                if (hasKeysHouse1 == true)
                {
                   OpenDoor(door1);
                    inventory = character.GetInventory();
                    inventory.RemoveItemType(Item.ItemType.KeyHouse1);
                }
                else
                {
                    character.DisplayMessage("This door is locked. To open you must find three keys. Challenges and enemies you must face to get them.");
                }
            }

            if (this.CompareTag("House2"))
            {
                if (monsterOrange == null && monsterPurple == null && monsterBlue == null)
                {
                    OpenDoor(door2);
                }
                else
                {
                    character.DisplayMessage("You must kill all the three monsters.");
                }
            }
        }

    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            Character character = collider.GetComponent<Character>();
            character.DisplayMessage("");
            if (this.CompareTag("House1")){
               CloseDoor(door1);
            }
            if (this.CompareTag("House2"))
            {
                CloseDoor(door2);
            }
        }
    }
}