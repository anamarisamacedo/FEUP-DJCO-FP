using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static utils.Configs;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public Rigidbody rigidBody;
    public CapsuleCollider capsuleCollider;
    public int rotateDirection;
    public bool isGrounded;
    public LayerMask groundLayers, monsterLayers;

    private CharacterState state;
    private Inventory inventory;
    [SerializeField] public UI_Inventory uiInventory; // TODO
    [SerializeField] public Health hp;
    [SerializeField] public Hunger hunger;
    [SerializeField] public BloodEffect blood;
    public Text textElement;
    public string message;
    bool hasKeysHouse1 = false;
    public bool collideClue1 = false;
    private float distance;
    private float someDistance = 3f;
    private GameObject clue;

    public Character() : base() { }

    private void Start()
    {
        state = new IdleState(this);
        rigidBody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        inventory = new Inventory();
        uiInventory.SetInventory(inventory);
        clue = GameObject.Find("Clipboard");
    }

    private void Update()
    {
        state.HandleInput();
        textElement.text = message;
        
        distance = Vector3.Distance(transform.position, clue.transform.position);
        if (distance < someDistance)
        {
            if (clue.CompareTag("Clue1"))
            {
                collideClue1 = true;
            }
        }
    }


    private void FixedUpdate()
    {
        state.MoveForward();
        Rotate();
    }

    public void ChangeState(CharacterState state)
    {
        this.state = state;
    }

    private void Rotate()
    {
        transform.Rotate(Vector3.up * RotationSpeed * rotateDirection);
    }

    public void TakeDamage(int value)
    {
        hp.ChangeValue(-value);
        StartCoroutine(blood.TakeDamage());
    }

    public void Heal(int value)
    {
        hp.ChangeValue(value);
        blood.Heal();
    }

    public void IncreaseHunger(int value)
    {
        hunger.ChangeValue(-value);
    }

    private void OnCollisionStay()
    {
        isGrounded = true;
    }

    private void OnCollisionExit()
    {
        isGrounded = false;
    }

    public void DisplayMessage(string new_message)
    {
        message = new_message;
    }

    private void OnTriggerEnter(Collider collider)
    {
            if (collider.CompareTag("House1"))
        {
            if (inventory.GetItemAmount(Item.ItemType.KeyHouse1) >= 3)
            {
                hasKeysHouse1 = true;
                inventory.RemoveItemType(Item.ItemType.KeyHouse1);
            }
            if(hasKeysHouse1 == true) { 
                HouseDoor houseDoor = collider.GetComponent<HouseDoor>();
                if (houseDoor != null)
                {
                    houseDoor.OpenDoor();
                }
            }
            else { 
                DisplayMessage("Door is locked...");
            }
        }

        if (collider.CompareTag("Clue1"))
        {
            DisplayMessage("Press R.");
        }

        WorldItem worldItem = collider.GetComponent<WorldItem>();
        if (worldItem != null)
        {
            inventory.AddItem(worldItem.GetItem());
            worldItem.DestroySelf();
        }
    }
    
    public Inventory GetInventory()
    {
        return inventory;
    }
    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("House1"))
        {
            DisplayMessage("");
            HouseDoor houseDoor = collider.GetComponent<HouseDoor>();
            if (houseDoor != null)
            {
                houseDoor.CloseDoor();
            }
        }
        if (collider.CompareTag("Clue1"))
        {
            collideClue1 = false;
            DisplayMessage("");
        }
    }
}
