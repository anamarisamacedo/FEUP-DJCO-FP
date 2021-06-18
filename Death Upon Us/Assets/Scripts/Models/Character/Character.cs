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
    [SerializeField] private UI_Input_Button mainInputButton;
    public Text textElement;
    public string message;
    private bool hasKeysHouse1 = false;
    private bool hasCodeVault1 = false;
    private GameObject clue;
    public Conversation convoNeedVault;
    public Conversation convoHaveVault;
    public string generatedCode;
    private string generatedPIN = "124";
    public bool hasDecoded = false;
    public bool inputEnabled = true;
    [SerializeField] private Animator dialogue;
    public bool isGirl = true;
    public TerrainUtils tu;
    FMOD.Studio.EventInstance snapshot;
    private bool inside = false;
    private bool mapFound = false;

    private int currentHP = 100;

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
        if (inside != tu.insideHouse(this.transform.position))
        {
            inside = !inside;
            if (inside)
            {
                //update snapshot for indoors
                snapshot = FMODUnity.RuntimeManager.CreateInstance("snapshot:/Indoors");
                snapshot.start();
            }
            else
            {
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
        this.currentHP -= value;
        if (currentHP <= 0)
        {
            FMOD.Studio.EventInstance instance1 = FMODUnity.RuntimeManager.CreateInstance("event:/Player/Die");

            if (isGirl)
            {
                instance1.setParameterByName("Character", 1);
            }
            else
            {
                instance1.setParameterByName("Character", 0);
            }
            instance1.start();
            instance1.release();
        }

        StartCoroutine(blood.TakeDamage());

        FMOD.Studio.EventInstance instance = FMODUnity.RuntimeManager.CreateInstance("event:/Player/TakeDamage");

        if (isGirl)
        {
            instance.setParameterByName("Character", 0);
        }
        else
        {
            instance.setParameterByName("Character", 1);
        }
        instance.start();
        instance.release();
    }

    public void AddHealth(int value)
    {
        hp.ChangeValue(value);
    }

    public void TakeHunger(int value)
    {
        hunger.ChangeValue(value);
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/Eat");
    }

    public void IncreaseHunger(int value)
    {
        hunger.ChangeValue(-value);
    }

    public int GetHungerValue()
    {
        return hunger.GetValue();
    }

    public void Attack()
    {
        StartCoroutine(PlayMeleeAnimation());
        Collider[] hitMonsters = Physics.OverlapSphere(transform.position, PlayerAttackRadius, monsterLayers);
        Debug.Log(hitMonsters);
        foreach (Collider monster in hitMonsters)
        {
            Debug.Log(monster);
            monster.gameObject.GetComponent<Monster>().TakeDamage(KnifeDamage);
        }
        IncreaseHunger(HungerOnMeleeAttack);
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/KnifeAttack");
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
            Sprite mapSprite = collider.gameObject.GetComponent<SpriteRenderer>().sprite;
            transform.Find("Canvas/Map").GetComponent<Image>().sprite = mapSprite;
            Destroy(collider.gameObject);
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
        if (isJumping)
        {
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

    public void CloseDialogue()
    {
        dialogue.SetBool("isDialogueOpen", false);
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

    public void SetGeneratedCode(string code)
    {
        this.generatedCode = code;
    }

    public void SetIsGirl(bool girl)
    {
        this.isGirl = girl;
    }

}
