using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static utils.Configs;

public class Character : MonoBehaviour
{
    public Rigidbody rigidBody;
    public CapsuleCollider capsuleCollider;
    public int rotateDirection;
    public bool isGrounded;
    public LayerMask groundLayers;

    private CharacterState state;
    private Inventory inventory;
    [SerializeField] private UI_Inventory uiInventory; // TODO
    [SerializeField] private Health hp;
    [SerializeField] private Hunger hunger;

    public Character() : base() { }

    private void Start()
    {
        state = new IdleState(this);
        rigidBody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
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

    private void OnTriggerEnter(Collider collider)
    {
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
}
