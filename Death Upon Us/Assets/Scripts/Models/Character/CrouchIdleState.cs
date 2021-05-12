using UnityEngine;

public class CrouchIdleState : CharacterState
{
    public CrouchIdleState(Character character) : base(character) { }

    public override void HandleKeyboardInput()
    {
        base.HandleKeyboardInput();
        
        if (Input.GetKeyDown(KeyCode.C))
        {
            character.ChangeState(new IdleState(character));
        }
        else if (Input.GetKey(KeyCode.W))
        {
            character.ChangeState(new CrouchWalkingState(character));
        }
    }
}