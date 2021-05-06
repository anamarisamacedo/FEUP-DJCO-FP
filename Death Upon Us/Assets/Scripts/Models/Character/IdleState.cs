using UnityEngine;

public class IdleState : CharacterState
{
    public IdleState(Character character) : base(character) { }

    public override void HandleInput()
    {
        base.HandleInput();
        if (Input.GetKeyDown(KeyCode.C))
        {
            character.ChangeState(new CrouchIdleState(character));
        }

        else if (Input.GetButton("Vertical"))
        {
            character.ChangeState(new WalkingState(character));
        }
        else
        {
            character.ChangeState(new IdleState(character));
        }
    }
}