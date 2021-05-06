using UnityEngine;
using static utils.Configs;

public class RunningState : CharacterState
{
    public RunningState(Character character) : base(character) { }

    public override void MoveForward()
    {
        character.transform.position += character.transform.forward * Time.deltaTime * RunningSpeed;
    }

    public override void HandleInput()
    {
        base.HandleInput();

        if (!Input.GetButton("Vertical"))
        {
            character.ChangeState(new IdleState(character));
        }

        else if (!Input.GetKey(KeyCode.LeftShift))
        {
            character.ChangeState(new WalkingState(character));
        }
    }
}