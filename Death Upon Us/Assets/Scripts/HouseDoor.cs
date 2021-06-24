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
    public bool wood = true;
    private bool vaultIsOpened = false;

    private bool isCodeCorrect = false;
    private string secretCode = "I043TKBR";
    [SerializeField] private UI_Input_Field mainInputField;
    Sprite mapSprite;
    private GameObject boy;

    private void Start(){
        boy = GameObject.Find("BoyCharacter");
        mapSprite = Resources.Load<Sprite>("fullMapBoy");
    }
    
    public void OpenDoor(Animator door)
    {
        FMOD.Studio.EventInstance instance = FMODUnity.RuntimeManager.CreateInstance("event:/Environment/DoorOpen");
        if (wood){
            instance.setParameterByName("Material", 0);
        }
        else{
            instance.setParameterByName("Material", 1);
        }
        instance.start();
        instance.release();

        door.SetBool("isOpen", true);
    }

    public void CloseDoor(Animator door)
    {
        FMOD.Studio.EventInstance instance = FMODUnity.RuntimeManager.CreateInstance("event:/Environment/DoorClose");;
        if (wood){
            instance.setParameterByName("Material", 0);
        }
        else{
            instance.setParameterByName("Material", 1);
        }
        instance.start();
        instance.release();

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
                    EnableInputField();
                }

            }

            if (this.CompareTag("CodeNumberBox"))
            {
                if (this.hasDecoded == false)
                {
                    EnableInputField();
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
                DisableInputField();
            }

            if (this.CompareTag("CodeNumberBox"))
            {
                DisableInputField();
            }
        }
    }

    public void EnterInputField()
    {
        DisableInputField();
        string valueCode = mainInputField.inputField.text;
        if (valueCode == boy.GetComponent<Character>().GetGeneratedCode())
        {
            isCodeCorrect = true;
            GameObject key = GameObject.FindGameObjectsWithTag("VaultKey")[0];
            WorldItem worldItem = key.GetComponent<WorldItem>();
            inventory.AddItem(worldItem.GetItem());
            worldItem.DestroySelf();
            this.hasKeyVault = true;
            OpenDoor(door);
        }
        else
        {
            character.DisplayMessage("Code is not correct! Try again.");
        }

        
    }

    public void EnterSecretCode()
    {
        DisableInputField();
        string valueCode = mainInputField.inputField.text;
        if (valueCode == secretCode)
        {
            character.DisplayMessage("Congratulations, you have found the completed map to get to resistance.");
            GameObject[] borders = GameObject.FindGameObjectsWithTag("BorderLevel1Boy");
            foreach (GameObject border in borders)
            {
                Destroy(border);
            }
            boy.GetComponent<Character>().transform.Find("Canvas/Map").GetComponent<Image>().sprite = mapSprite;
        }
        else
        {
            character.DisplayMessage("Secret code is not correct! Try again.");
        }

        
    }

    public void EnableInputField()
    {
        mainInputField.Show();
        character.inputEnabled = false;
        Cursor.visible = true;
    }

    public void DisableInputField()
    {
        mainInputField.Hide();
        character.inputEnabled = true;
        Cursor.visible = false;
    }
   
}