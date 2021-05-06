using UnityEngine;
using static utils.Configs;

public class CrouchWalkingState : CharacterState
{
    public CrouchWalkingState(Character character) : base(character) { }

    public override void MoveForward()
    {
        character.transform.position += character.transform.forward * Time.deltaTime * CrouchSpeed;
    }

    public override void HandleKeyBoardInput()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            character.ChangeState(new WalkingState(character));
        }

        if (!Input.GetKey(KeyCode.W))
        {
            character.ChangeState(new CrouchIdleState(character));
        }
    }
    public override void Jump() { }
}