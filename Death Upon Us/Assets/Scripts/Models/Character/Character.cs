using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static utils.Configs;

public class Character : MonoBehaviour
{
    public Rigidbody rigidBody;
    public int rotateDirection;
    public bool isGrounded;

    private CharacterState state;
    private Inventory inventory;
    [SerializeField] private UI_Inventory uiInventory; // TODO
    [SerializeField] private Health hp;

    public Character() : base() {}

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
    }
    
    private void FixedUpdate()
    {
        state.MoveForward();
        state.Jump();
        Rotate();
    }

    public void ChangeState(CharacterState state) {
        this.state = state;
    }
    
    private void Rotate()
    {
        transform.Rotate(Vector3.up * RotationSpeed * rotateDirection);
    }

    public void TakeDamage(int value) {
        hp.ChangeHealth(-value);
    }

    public void Heal(int value) {
        hp.ChangeHealth(value);
    }

    private void OnCollisionStay()
    {
        isGrounded = true;
    }

    private void OnCollisionExit()
    {
        isGrounded = false;
    }

    private void OnTriggerEnter(Collider collider)
    {
        WorldItem worldItem = collider.GetComponent<WorldItem>();
        if (worldItem != null)
        {
            inventory.AddItem(worldItem.GetItem());
            worldItem.DestroySelf();
        }
    }
}
