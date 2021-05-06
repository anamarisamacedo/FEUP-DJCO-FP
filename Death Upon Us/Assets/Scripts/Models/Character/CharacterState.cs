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
        HandleKeyBoardInput();
    }

    public virtual void HandleKeyBoardInput() { }

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