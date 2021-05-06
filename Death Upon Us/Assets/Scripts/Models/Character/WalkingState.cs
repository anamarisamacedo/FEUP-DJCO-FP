using UnityEngine;
using static utils.Configs;


public class WalkingState : CharacterState
{
    public WalkingState(Character character) : base(character) { }

    public override void MoveForward()
    {
        character.transform.position += character.transform.forward * Time.deltaTime * WalkingSpeed;
    }
    public override void HandleInput()
    {
        base.HandleInput();
        if (Input.GetKeyDown(KeyCode.C))
        {
            character.ChangeState(new CrouchWalkingState(character));
        }

        else if (!Input.GetButton("Vertical"))
        {
            character.ChangeState(new IdleState(character));
        }

        else if (Input.GetKey(KeyCode.LeftShift))
        {
            character.ChangeState(new RunningState(character));
        }
    }
}