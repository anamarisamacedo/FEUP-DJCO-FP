using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static utils.Configs;
using UnityEngine.UI;

public abstract class CharacterState
{
    protected Character character;


    public CharacterState(Character character)
    {
        this.character = character;
    }

    public Character CurrentCharacter()
    {
        return this.character;
    }

    public float jumpForce = 7f;

    public virtual void MoveForward() { }

    public void Jump()
    {
        if (IsGrounded())
        {
            character.GetComponent<Animator>().SetBool("IsJumping", false);
        } 
        else 
        {
            return;
        }

        if (character.IsJumping() && character.GetComponent<Animator>().GetInteger("State") < 3)
        {
            character.GetComponent<Animator>().SetBool("IsJumping", true);
            character.rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        character.SetIsJumping(false);
    }
    
    private bool IsGrounded()
    {
        CapsuleCollider col = character.capsuleCollider;
        return Physics.CheckCapsule(col.bounds.center, new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z), col.radius * 0.9f,
            character.groundLayers);
    }

    public void HandleInput()
    {
        HandleMouseInput();
        HandleKeyboardInput();
    }

    public virtual void HandleKeyboardInput()
    {
        if (Time.timeScale == 1 && character.inputEnabled)
        {
            if (Input.GetKey(KeyCode.A))
            {
                character.rotateDirection = -1;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                character.rotateDirection = 1;
            }
            else if (Input.GetKeyDown(KeyCode.M))
            {
                Image img = character.transform.Find("Canvas/Map").GetComponent<Image>();
                img.enabled = !img.enabled;
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                character.SetIsJumping(true);
            }
            else if (Input.GetKey(KeyCode.Alpha1))
            {
                character.GetInventory().SelectItem(1);
            }
            else if (Input.GetKey(KeyCode.Alpha2))
            {
                character.GetInventory().SelectItem(2);
            }
            else if (Input.GetKey(KeyCode.Alpha3))
            {
                character.GetInventory().SelectItem(3);
            }
            else if (Input.GetKey(KeyCode.Alpha4))
            {
                character.GetInventory().SelectItem(4);
            }
            else if (Input.GetKey(KeyCode.Alpha5))
            {
                character.GetInventory().SelectItem(5);
            }
            else if (Input.GetKey(KeyCode.Alpha6))
            {
                character.GetInventory().SelectItem(6);
            }
            else if (Input.GetKey(KeyCode.Alpha7))
            {
                character.GetInventory().SelectItem(7);
            }
            else if (Input.GetKey(KeyCode.Alpha8))
            {
                character.GetInventory().SelectItem(8);
            }
            else if (Input.GetKey(KeyCode.Alpha9))
            {
                character.GetInventory().SelectItem(9);
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                character.GetInventory().UseItem(character);
            }
        }
    }

    public virtual void HandleMouseInput()
    {
        if (Time.timeScale == 1)
        {
            float mouseDelta = Input.GetAxis("Mouse X");
            if (mouseDelta != 0)
            {
                character.rotateDirection = (mouseDelta < 0 ? -1 : 1);
            }
            else
            {
                character.rotateDirection = 0;
            }

            if (Input.GetButtonDown("Fire1"))
            {
                character.GetInventory().UseItem(character);
            }
        }
    }

    public virtual void ChangeAnimation() { }

}