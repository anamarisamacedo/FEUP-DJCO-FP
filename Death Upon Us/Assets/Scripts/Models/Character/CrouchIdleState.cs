using UnityEngine;

public class CrouchIdleState : CharacterState
{
    public CrouchIdleState(Character character) : base(character) { }
    public override void HandleKeyBoardInput()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            character.ChangeState(new IdleState(character));
        }
        else if (Input.GetButton("Vertical"))
        {
            character.ChangeState(new CrouchWalkingState(character));
        }
    }
    public override void Jump() { }
}