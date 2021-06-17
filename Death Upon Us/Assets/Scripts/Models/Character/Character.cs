using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static utils.Configs;
using UnityEngine.UI;
using utils;

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
    private GameObject clue;
    public Conversation convoNeedVault;
    public Conversation convoHaveVault;
    private bool isCodeCorrect;
    private string generatedCode;
    private string generatedPIN = "134";
    public bool hasDecoded = false;
    public bool inputEnabled = true;
    [SerializeField] private Animator dialogue;
    public bool isGirl = false;
    public TerrainUtils tu;
    FMOD.Studio.EventInstance snapshot;
    private bool inside = false;
    private bool mapFound = false;

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
        tu = new TerrainUtils();
    }

    private void Update()
    {
        state.HandleInput();
        textElement.text = message;
        
        if (inventory.GetItemAmount(Item.ItemType.KeyHouse1) >= 2)
        {
            hasKeysHouse1 = true;
        }

        if (this.mapFound == true)
        {
            GameObject[] borders = GameObject.FindGameObjectsWithTag("BorderLevel1Girl");
            foreach (GameObject border in borders)
            {
                Destroy(border);
            }
        }

        /*if(this.hasDecoded){
         GameObject[] borders = GameObject.FindGameObjectsWithTag("BorderLevel1Boy");
            foreach (GameObject border in borders)
            {
                Destroy(border);
            }
          }
         */

        if (inside != tu.insideHouse(this.transform.position)){
            inside = !inside;
            if (inside){
                //update snapshot for indoors
                snapshot = FMODUnity.RuntimeManager.CreateInstance("snapshot:/Indoors");
                snapshot.start();
            }
            else{
                snapshot = FMODUnity.RuntimeManager.CreateInstance("snapshot:/Outdoors");
                snapshot.start();
            }
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

        FMOD.Studio.EventInstance instance = FMODUnity.RuntimeManager.CreateInstance("event:/Player/TakeDamage");
    
        if (isGirl){
            instance.setParameterByName("Character", 1);
        }
        else{
            instance.setParameterByName("Character", 0);
        }
        instance.start();
        instance.release();
    }

    public void AddHealth(int value)
    {
        hp.ChangeValue(value);
    }

    public void TakeHunger(int value) {
        hunger.ChangeValue(value);
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/Eat");
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
        if (true) //knife attack)
            FMODUnity.RuntimeManager.PlayOneShot("event:/Player/KnifeAttack");
        else
            FMODUnity.RuntimeManager.PlayOneShot("event:/Player/BowAttack");
    }

    private IEnumerator PlayMeleeAnimation()
    {
        gameObject.GetComponentInChildren<MeshRenderer>().enabled = true;
        GetComponent<Animator>().SetBool("MeleeAttack", true);
        yield return new WaitForSeconds(1.2f);
        gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
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
            DisplayMessage("The vault code is " + this.generatedCode);
        }

        if (collider.CompareTag("Clue2"))
        {
            DisplayMessage("The buttons order is " + this.generatedPIN);
        }

        WorldItem worldItem = collider.GetComponent<WorldItem>();
        if (worldItem != null)
        {
            inventory.AddItem(worldItem.GetItem());
            worldItem.DestroySelf();
        }

        if (collider.CompareTag("Map"))
        {
            DisplayMessage("Congrats! You've found a new map with new information! Check it out.");
            this.mapFound = true;
            //Destroy(collider.gameObject);
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
        if(isJumping){
            if (!this.isJumping)
                FMODUnity.RuntimeManager.PlayOneShot("event:/Player/Jump");
            IncreaseHunger(HungerOnJump);
        }
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
        this.inputEnabled = false;
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
        this.inputEnabled = true;
    }

    public void EnableInputButton()
    {
        mainInputButton.Show();
        this.inputEnabled = false;
    }

    public void EnterInputButton()
    {
        mainInputButton.Hide();
        bool[] sequence = mainInputButton.GetSequence();
        this.hasDecoded = true;
        DisplayMessage("You deciphered the code!");
    }

    public void DisableInputButton()
    {
        mainInputButton.Hide();
        this.inputEnabled = true;
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
