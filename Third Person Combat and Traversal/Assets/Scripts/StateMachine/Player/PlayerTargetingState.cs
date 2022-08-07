using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    readonly int targetingBlendTreeHash = Animator.StringToHash("TargetingBlendTree");
    readonly int targetForwardHash = Animator.StringToHash("TargetForward");
    readonly int targetRightHash = Animator.StringToHash("TargetRight");

    const float animTransitionSmoothingVal = 0.1f;

    public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine){}

    public override void Enter()
    {
        stateMachine.InputReader.TargetEvent += OnCancel;
        stateMachine.InputReader.DodgeEvent += HandleDodge;

        stateMachine.Animator.CrossFadeInFixedTime(targetingBlendTreeHash, 0.1f);
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.InputReader.IsAttacking)
        {
            stateMachine.SwitchState(new PlayerAttackingState(stateMachine, 0));
            return;
        }
        if(stateMachine.Targeter.CurrentTarget == null)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            return;
        }
        if (stateMachine.InputReader.IsBlocking)
        {
            stateMachine.SwitchState(new PlayerBlockingState(stateMachine));
            return;
        }

        Vector3 direction = CalculateDirection(deltaTime);

        Move(direction * stateMachine.TargetLookSpeed, deltaTime);

        UpdateAnimator(deltaTime);
        FaceTarget();
    }

    public override void Exit()
    {
        stateMachine.InputReader.TargetEvent -= OnCancel;
        stateMachine.InputReader.DodgeEvent -= HandleDodge;
    }

    private void OnCancel()
    {
        stateMachine.Targeter.Cancel();
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }

    private void HandleDodge()
    {
        if (stateMachine.InputReader.MovementValue == Vector2.zero) return;

        stateMachine.SwitchState(new PlayerDodgingState(stateMachine, stateMachine.InputReader.MovementValue));
    }

    Vector3 CalculateDirection(float deltaTime)
    {
        Vector3 direction = new();

        direction += stateMachine.InputReader.MovementValue.x * stateMachine.transform.right;
        direction += stateMachine.InputReader.MovementValue.y * stateMachine.transform.forward;

        return direction;
    }

    void UpdateAnimator(float deltaTime)
    {
        float inputX = stateMachine.InputReader.MovementValue.x;
        float inputY = stateMachine.InputReader.MovementValue.y;

        stateMachine.Animator.SetFloat(targetForwardHash, inputY, animTransitionSmoothingVal, deltaTime);
        stateMachine.Animator.SetFloat(targetRightHash, inputX, animTransitionSmoothingVal, deltaTime);
    }
}
