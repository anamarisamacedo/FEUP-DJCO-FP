using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static utils.Configs;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public Rigidbody rigidBody;
    public int rotateDirection;
    public bool isGrounded;

    private CharacterState state;
    private Inventory inventory;
    [SerializeField] private UI_Inventory uiInventory; // TODO
    [SerializeField] private Health hp;
    [SerializeField] private Hunger hunger;
    public Text textElement;
    public string message;
    bool hasKeysHouse1 = false;

    public Character() : base() { }

    private void Start()
    {
        state = new IdleState(this);
        rigidBody = GetComponent<Rigidbody>();
        inventory = new Inventory();
        uiInventory.SetInventory(inventory);
    }

    private void Update()
    {
        state.HandleInput();
        textElement.text = message;
    }

    private void FixedUpdate()
    {
        state.MoveForward();
        state.Jump();
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
    }

    public void Heal(int value)
    {
        hp.ChangeValue(value);
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

    IEnumerator displayMessage(string new_message)
    {
        message = new_message;
        yield return new WaitForSeconds(3);
        message = "";
    }

    private void OnTriggerEnter(Collider collider)
    {

        if (collider.CompareTag("House1"))
        {           
            if (inventory.GetItemAmount(Item.ItemType.KeyHouse1) >= 3)
            {
                hasKeysHouse1 = true;
                HouseDoor houseDoor = collider.GetComponent<HouseDoor>();
                if (houseDoor != null)
                {
                    houseDoor.openDoor();
                    inventory.RemoveItemType(Item.ItemType.KeyHouse1);
                }
            }
            if(hasKeysHouse1 == false){
                StartCoroutine(displayMessage("Door is locked..."));
            }
        }

        WorldItem worldItem = collider.GetComponent<WorldItem>();
        if (worldItem != null)
        {
            inventory.AddItem(worldItem.GetItem());
            worldItem.DestroySelf();
        }
    }

    /*
    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("House1"))
        {
            HouseDoor houseDoor = collider.GetComponent<HouseDoor>();
            if (houseDoor != null)
            {
                houseDoor.closeDoor();
            }
        }
    }*/
}
