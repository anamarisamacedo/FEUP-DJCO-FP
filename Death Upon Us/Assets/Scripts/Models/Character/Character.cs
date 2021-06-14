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
    private bool isJumping;
    public LayerMask groundLayers, monsterLayers;

    private CharacterState state;
    private Inventory inventory;
    [SerializeField] public UI_Inventory uiInventory;
    [SerializeField] public Health hp;
    [SerializeField] public Hunger hunger;
    [SerializeField] public BloodEffect blood;
    public Text textElement;
    public string message;
    private bool hasKeysHouse1 = false;
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
        isJumping = false;
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

        if (inventory.GetItemAmount(Item.ItemType.KeyHouse1) >= 3)
        {
            hasKeysHouse1 = true;
        }
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
        this.state.ChangeAnimation();
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

    public void AddHealth(int value)
    {
        hp.ChangeValue(value);
    }

    public void TakeHunger(int value) {
        hunger.ChangeValue(value);
    }

    public void IncreaseHunger(int value) {
        hunger.ChangeValue(-value);
    }

    public int GetHungerValue() {
        return hunger.GetValue();
    }

    public void Attack() {
        StartCoroutine(PlayMeleeAnimation());
        Collider[] hitMonsters = Physics.OverlapSphere(transform.position, PlayerAttackRadius, monsterLayers);
        foreach(Collider monster in hitMonsters) {
            monster.gameObject.GetComponent<Monster>().TakeDamage(35);
        }
        IncreaseHunger(HungerOnMeleeAttack);
    }

    private IEnumerator PlayMeleeAnimation()
    {
        GetComponent<Animator>().SetBool("MeleeAttack", true);
        yield return new WaitForSeconds(1.2f);
        GetComponent<Animator>().SetBool("MeleeAttack", false);
    }

    public void Heal(int value)
    {
        hp.ChangeValue(value);
        blood.Heal();
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
        if (collider.CompareTag("Clue1"))
        {
            collideClue1 = false;
            DisplayMessage("");
        }
    }
    public bool IsJumping()
    {
        return isJumping;
    }

    public void SetIsJumping(bool isJumping)
    {
        if(isJumping)
            IncreaseHunger(HungerOnJump);
        this.isJumping = isJumping;
    }

    public bool GetHasKeysHouse1()
    {
        return this.hasKeysHouse1;
    }
}
