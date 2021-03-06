using UnityEngine;
using static utils.Configs;
using utils;

public class CrouchWalkingState : CharacterState
{
    private FMOD.Studio.EventInstance instance;

    public CrouchWalkingState(Character character) : base(character)
    {
        instance = FMODUnity.RuntimeManager.CreateInstance("event:/Player/CrouchWalking");
        instance.setParameterByName("Terrain", character.tu.SelectFootstep(character.transform.position));
        instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(character.transform.parent.gameObject));
        instance.start();
    }

    public override void MoveForward()
    {
        character.transform.position += character.transform.forward * Time.deltaTime * CrouchSpeed;
        character.IncreaseHunger(HungerOnWalk);
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

        if (Input.GetKeyDown(KeyCode.E))
        {
            character.ChangeState(new WalkingState(character));
            StopSound();
        }
        else if (!Input.GetKey(KeyCode.W))
        {
            character.ChangeState(new CrouchIdleState(character));
            StopSound();
        }
    }

    public override void ChangeAnimation()
    {
        base.ChangeAnimation();
        character.GetComponent<Animator>().SetInteger("State", 4);
    }
}