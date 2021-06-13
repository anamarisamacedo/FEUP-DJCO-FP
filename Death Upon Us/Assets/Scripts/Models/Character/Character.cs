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
    GameObject girl;
    GameObject boy;

    private CharacterState state;
    private Inventory inventory;
    [SerializeField] public UI_Inventory uiInventory;
    [SerializeField] public Health hp;
    [SerializeField] public Hunger hunger;
    [SerializeField] public BloodEffect blood;
    [SerializeField] private UI_Input_Field mainInputField;
    [SerializeField] private UI_Input_Button mainInputButton;
    public Text textElement;
    public string message;
    private bool hasKeysHouse1 = false;
    private bool hasCodeVault1 = false;
    public bool collideClue1 = false;
    private GameObject clue;
    public Conversation convoNeedVault;
    public Conversation convoHaveVault;
    private bool isCodeCorrect;
    private string generatedCode;
    public bool hasDecoded = false;

    [SerializeField] private Animator dialogue;

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
        
        if (inventory.GetItemAmount(Item.ItemType.KeyHouse1) >= 2)
        {
            hasKeysHouse1 = true;
        }

        //TODO: Quando o mapa na House1 estiver adicionado ao inventário, o jogador já pode passar a fronteira para chegar ao fim
        /*if (inventory.GetItemAmount(Item.ItemType.FullMap) > 0)
        {
            GameObject[] borders = GameObject.FindGameObjectsWithTag("BorderLevel1Girl");
            foreach (GameObject border in borders)
            {
                Destroy(border);
            }
        }*/

        /*if(this.hasDecoded){
         GameObject[] borders = GameObject.FindGameObjectsWithTag("BorderLevel1Boy");
            foreach (GameObject border in borders)
            {
                Destroy(border);
            }
          }
         */
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
        Collider[] hitMonsters = Physics.OverlapSphere(transform.position, PlayerAttackRadius, monsterLayers);
        foreach(Collider monster in hitMonsters) {
            monster.gameObject.GetComponent<Monster>().TakeDamage(35);
        }
        IncreaseHunger(HungerOnMeleeAttack);
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
            DisplayMessage("The vault code is " + this.generatedCode);
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

    public bool GetHasCodeVault1()
    {
        return this.hasCodeVault1;
    }

    public void SetHasCodeVault1(bool hasCode)
    {
        this.hasCodeVault1 = hasCode;
    }

    public void StartDialogueVaultCode()
    {
        OpenDialogue();
        
        if (!hasCodeVault1)
        {
            DialogueManager.StartConversation(convoNeedVault);
        }
        else
        {
            DialogueManager.StartConversation(convoHaveVault);
        }
    }

    public void OpenDialogue()
    {
        dialogue.SetBool("isDialogueOpen", true);
    }

    public void CloseDialogue() { 
        dialogue.SetBool("isDialogueOpen", false);
    }

    public void EnableInputField()
    {
        mainInputField.Show();
    }

    public void EnterInputField()
    {
        mainInputField.Hide();

        string valueCode = mainInputField.inputField.text;
            if (valueCode == this.generatedCode)
            {
                this.isCodeCorrect = true;
                GameObject key = GameObject.FindGameObjectsWithTag("VaultKey")[0];
                WorldItem worldItem = key.GetComponent<WorldItem>();
                inventory.AddItem(worldItem.GetItem());
                worldItem.DestroySelf();
            }
            else
            {
                DisplayMessage("Code is not correct! Try again.");
            }
    }

    public void DisableInputField()
    {
        mainInputField.Hide();
    }

    public void EnableInputButton()
    {
        mainInputButton.Show();
    }

    public void EnterInputButton()
    {
        mainInputButton.Hide();

        this.hasDecoded = true;
        DisplayMessage("Code is not correct! Try again.");

    }

    public void DisableInputButton()
    {
        mainInputButton.Hide();
    }

    public bool IsCodeCorrect()
    {
        return this.isCodeCorrect;
    }

    public void SetGeneratedCode(string code)
    {
        this.generatedCode = code;
    }
}
