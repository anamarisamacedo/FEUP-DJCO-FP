using UnityEngine;
using static utils.Configs;

public class RunningState : CharacterState
{
    public RunningState(Character character) : base(character) { }

    public override void MoveForward()
    {
        character.transform.position += character.transform.forward * Time.deltaTime * RunningSpeed;
        character.IncreaseHunger(HungerOnRun);
    }

    public override void HandleKeyboardInput()
    {
        base.HandleKeyboardInput();

        if (!Input.GetKey(KeyCode.W))
        {
            character.ChangeState(new IdleState(character));
        }
        else if (!Input.GetKey(KeyCode.LeftShift))
        {
            character.ChangeState(new WalkingState(character));
        }
    }

    public override void ChangeAnimation()
    {
        base.ChangeAnimation();
        character.GetComponent<Animator>().SetInteger("State", 2);
    }
}