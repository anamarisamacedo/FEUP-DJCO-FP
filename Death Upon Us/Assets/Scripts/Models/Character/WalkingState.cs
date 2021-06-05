using UnityEngine;
using static utils.Configs;
using System;

public class WalkingState : CharacterState
{
    private FMOD.Studio.EventInstance instance;

    public WalkingState(Character character) : base(character)
    {
        instance = FMODUnity.RuntimeManager.CreateInstance("event:/Player/Walking");
        instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(character.transform.parent.gameObject));
        instance.start();
    }

    public override void MoveForward()
    {
        character.transform.position += character.transform.forward * Time.deltaTime * WalkingSpeed;
        character.IncreaseHunger(HungerOnWalk);
    }

    private void StopSound()
    {
        instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        instance.release();
    }

    public override void HandleKeyboardInput()
    {
        base.HandleKeyboardInput();

        if (Input.GetKeyDown(KeyCode.E))
        {
            character.ChangeState(new CrouchWalkingState(character));
            StopSound();
        }
        else if (!Input.GetKey(KeyCode.W))
        {
            character.ChangeState(new IdleState(character));
            StopSound();
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            if (character.GetHungerValue() > MinHungerValToRun)
            {
                character.ChangeState(new RunningState(character));
                StopSound();
            }
        }
    }
    public override void ChangeAnimation()
    {
        base.ChangeAnimation();
        character.GetComponent<Animator>().SetInteger("State", 1);
    }
}