using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static utils.Configs;

public abstract class CharacterState
{
    protected Character character;

    public CharacterState(Character character)
    {
        this.character = character;
    }

    public virtual void MoveForward() { }
    public virtual void Jump() { } // TODO

    public void HandleInput()
    {
        HandleMouseInput();
        HandleKeyboardInput();
    }

    public virtual void HandleKeyboardInput()
    {
        if (Input.GetKey(KeyCode.A))
        {
            character.TakeDamage(1); // Testing purposes
            character.rotateDirection = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            character.IncreaseHunger(1); // Testing purposes
            character.rotateDirection = 1;
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
    }

    public virtual void HandleMouseInput()
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
    }
}