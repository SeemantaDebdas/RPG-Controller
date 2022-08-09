using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPullupState : PlayerBaseState
{
    readonly int pullUpHash = Animator.StringToHash("Freehang Climb");
    const float crossFadeAnimDuration = 0.1f;

    public PlayerPullupState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(pullUpHash, crossFadeAnimDuration);
    }
    public override void Tick(float deltaTime)
    {
        if (GetNormalizedTime(stateMachine.Animator, "Climbing")< 1f) return;

        stateMachine.Controller.enabled = false;
        stateMachine.transform.Translate(0f, 2.325f, 0.65f, Space.Self);
        //stateMachine.transform.position = stateMachine.Animator.rootPosition;
        stateMachine.Controller.enabled = true;

        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine, false));
    }

    public override void Exit()
    {
        stateMachine.Controller.Move(Vector3.zero);
        stateMachine.ForceReceiver.Reset();
    }

}
