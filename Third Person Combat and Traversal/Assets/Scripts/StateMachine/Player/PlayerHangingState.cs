using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHangingState : PlayerBaseState
{
    readonly int hangingHash = Animator.StringToHash("Hanging");
    const float crossFadeAnimDuration = 0.1f;

    Vector3 ledgeForward;

    public PlayerHangingState(PlayerStateMachine stateMachine, Vector3 ledgeForward) : base(stateMachine)
    {
        this.ledgeForward = ledgeForward;
    }

    public override void Enter()
    {
        stateMachine.transform.rotation = Quaternion.LookRotation(ledgeForward, Vector3.up);
        stateMachine.Animator.CrossFadeInFixedTime(hangingHash, crossFadeAnimDuration);

        stateMachine.InputReader.JumpEvent += PullUp;
    }

    public override void Tick(float deltaTime)
    {
        //when the dodge button is pressed when hanging, fall down
        if (stateMachine.InputReader.IsBlocking)
        {
            stateMachine.ForceReceiver.Reset();
            stateMachine.SwitchState(new PlayerFallingState(stateMachine));
            return;
        }
    }

    public override void Exit()
    {
        stateMachine.InputReader.JumpEvent -= PullUp;
    }

    private void PullUp()
    {
        stateMachine.ForceReceiver.Reset();
        stateMachine.SwitchState(new PlayerPullupState(stateMachine));
    }
}
