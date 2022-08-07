using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    readonly int jumpHash = Animator.StringToHash("Jump");

    const float crossFadeDuration = 0.1f;

    Vector3 momentum;
    public PlayerJumpState(PlayerStateMachine stateMachine) : base(stateMachine){}

    public override void Enter()
    {
        stateMachine.ForceReceiver.Jump(stateMachine.JumpForce);

        momentum = stateMachine.Controller.velocity;
        momentum.y = 0f;
        stateMachine.Animator.CrossFadeInFixedTime(jumpHash, crossFadeDuration);
    }
    public override void Tick(float deltaTime)
    {
        Move(momentum, deltaTime);
        if(stateMachine.Controller.velocity.y <= 0f)
        {
            stateMachine.SwitchState(new PlayerFallState(stateMachine));
        }
    }

    public override void Exit()
    {
    }
}