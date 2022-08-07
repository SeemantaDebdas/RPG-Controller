using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgingState : PlayerBaseState
{
    readonly int dodgeBlendTreeHash = Animator.StringToHash("DodgeBlendTree");
    readonly int dodgeForwardHash = Animator.StringToHash("DodgeForward");
    readonly int dodgeRightHash = Animator.StringToHash("DodgeRight");

    Vector2 dodgeMovementInput;
    float remainingDodgeTime;

    const float crossFadeDuration = 0.1f;
    public PlayerDodgingState(PlayerStateMachine stateMachine, Vector3 dodgeMovementInput) : base(stateMachine)
    {
        this.dodgeMovementInput = dodgeMovementInput;
    }

    public override void Enter()
    {
        remainingDodgeTime = stateMachine.DodgeTime;

        stateMachine.Animator.SetFloat(dodgeRightHash, dodgeMovementInput.x);
        stateMachine.Animator.SetFloat(dodgeForwardHash, dodgeMovementInput.y);
        stateMachine.Animator.CrossFadeInFixedTime(dodgeBlendTreeHash, crossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        Vector3 direction = new();

        float dodgeSpeed = stateMachine.DodgeDistance / stateMachine.DodgeTime;
        direction += dodgeMovementInput.x * dodgeSpeed * stateMachine.transform.right;
        direction += dodgeMovementInput.y * dodgeSpeed * stateMachine.transform.forward;

        Move(direction, deltaTime);

        FaceTarget();

        remainingDodgeTime = Mathf.Max(0, remainingDodgeTime - deltaTime);
        if(remainingDodgeTime <= 0f)
        {
            stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
        }
    }

    public override void Exit()
    {
    }
}
