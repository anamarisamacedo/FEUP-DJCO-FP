using UnityEngine;
using static utils.Configs;
using utils;

public class RunningState : CharacterState
{
    private FMOD.Studio.EventInstance instance;

    public RunningState(Character character) : base(character)
    {
        instance = FMODUnity.RuntimeManager.CreateInstance("event:/Player/Running");
        instance.setParameterByName("Terrain", character.tu.SelectFootstep(character.transform.position));
        instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(character.transform.parent.gameObject));
        instance.start();
    }

    public override void MoveForward()
    {
        character.transform.position += character.transform.forward * Time.deltaTime * RunningSpeed;
        character.IncreaseHunger(HungerOnRun);
        if (character.GetHungerValue() < MinHungerValToRun)
        {
            character.ChangeState(new WalkingState(character));
        }
        instance.setParameterByName("Terrain", character.tu.SelectFootstep(character.transform.position));
    }

    private void StopSound()
    {
        instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        instance.release();
    }

    public override void HandleKeyboardInput()
    {
        base.HandleKeyboardInput();

        if (!Input.GetKey(KeyCode.W))
        {
            character.ChangeState(new IdleState(character));
            StopSound();
        }
        else if (!Input.GetKey(KeyCode.LeftShift))
        {
            character.ChangeState(new WalkingState(character));
            StopSound();
        }
    }

    public override void ChangeAnimation()
    {
        base.ChangeAnimation();
        character.GetComponent<Animator>().SetInteger("State", 2);
    }
}