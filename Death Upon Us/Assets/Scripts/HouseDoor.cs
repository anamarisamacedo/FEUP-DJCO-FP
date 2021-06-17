using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using static utils.Configs;

public class HouseDoor : MonoBehaviour
{
    [SerializeField] private Animator door = null;
    [SerializeField] private GameObject? monsterOrange;
    [SerializeField] private GameObject? monsterPurple;

    [SerializeField] private GameObject? monsterBlue;
    bool hasKeysHouse1 = false;
    bool hasCodeVault1 = false;
    private Inventory inventory;
    private bool hasKeyVault = false;
    private bool hasDecoded = false;
    Character character;

    private void Update()
    {
        if (character != null)
        {
            bool codeAnswer = character.IsCodeCorrect();
            bool hasDecoded = character.hasDecoded;
            if (codeAnswer)
            {
                OpenDoor(door);
                this.hasKeyVault = true;
            }
        }
    }
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
            character = collider.GetComponent<Character>();
            inventory = character.GetInventory();
            if (this.CompareTag("House1"))
            {
                hasKeysHouse1 = character.GetHasKeysHouse1();
                if (hasKeysHouse1 == true)
                {
                    OpenDoor(door);
                    inventory.RemoveItemType(Item.ItemType.KeyHouse1);
                }
                else
                {
                    character.DisplayMessage("This door is locked. To open you must find two keys. Challenges and enemies you must face to get them.");
                }
            }

            if (this.CompareTag("House2"))
            {
                if (monsterOrange == null && monsterPurple == null && monsterBlue == null)
                {
                    OpenDoor(door);
                }
                else
                {
                    character.DisplayMessage("You must kill all the three monsters.");
                }
            }

            if (this.CompareTag("House3"))
            {
                OpenDoor(door);
            }

            if (this.CompareTag("Cofre1"))
            {
                if (this.hasKeyVault == false)
                {
                    character.EnableInputField();
                }

            }

            if (this.CompareTag("CodeNumberBox"))
            {
                if (this.hasDecoded == false)
                {
                    character.EnableInputButton();
                }

            }

            if (this.CompareTag("House4"))
            {
                OpenDoor(door);
            }

        }

    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            Character character = collider.GetComponent<Character>();
            character.DisplayMessage("");
            if (this.CompareTag("House1"))
            {
                if (hasKeysHouse1 == true)
                {
                    CloseDoor(door);
                }
            }

            if(this.CompareTag("House2"))
            {
                if (monsterOrange == null && monsterPurple == null && monsterBlue == null)
                {
                    CloseDoor(door);
                }
            }

            if(this.CompareTag("House3") || this.CompareTag("House4"))
            {
                CloseDoor(door);
            }

            if (this.CompareTag("Cofre1"))
            {
                character.DisableInputField();
            }

            if (this.CompareTag("CodeNumberBox"))
            {
                character.DisableInputButton();
            }
        }
    }

   
}