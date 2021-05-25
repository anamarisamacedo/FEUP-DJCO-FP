using UnityEngine;

public class CrouchIdleState : CharacterState
{
    public CrouchIdleState(Character character) : base(character) { }

    public override void HandleKeyboardInput()
    {
        base.HandleKeyboardInput();
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            character.ChangeState(new IdleState(character));
        }
        else if (Input.GetKey(KeyCode.W))
        {
            character.ChangeState(new CrouchWalkingState(character));
        }
    }

    public override void ChangeAnimation()
    {
        base.ChangeAnimation();
        character.GetComponent<Animator>().SetInteger("State", 3);
    }
}