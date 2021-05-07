using UnityEngine;

public class IdleState : CharacterState
{
    public IdleState(Character character) : base(character) { }

    public override void HandleKeyboardInput()
    {
        base.HandleKeyboardInput();

        if (Input.GetKeyDown(KeyCode.C))
        {
            character.ChangeState(new CrouchIdleState(character));
        }
        else if (Input.GetKey(KeyCode.W))
        {
            character.ChangeState(new WalkingState(character));
        }
        else
        {
            character.ChangeState(new IdleState(character));
        }
    }
}