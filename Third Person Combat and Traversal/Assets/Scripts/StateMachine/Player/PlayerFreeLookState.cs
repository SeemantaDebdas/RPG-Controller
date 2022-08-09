using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{
    readonly int freeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");
    readonly int freeLookBlendTreeHash = Animator.StringToHash("FreeLookBlendTree");
    const float animTransitionSmoothingVal = 0.1f;

    bool shouldFade = false;

    public PlayerFreeLookState(PlayerStateMachine stateMachine, bool shouldFade = true) : base(stateMachine)
    {
        this.shouldFade = shouldFade;
    }

    public override void Enter()
    {
        stateMachine.InputReader.TargetEvent += OnTarget;
        stateMachine.InputReader.JumpEvent += OnJump;
        if(shouldFade)
            stateMachine.Animator.CrossFadeInFixedTime(freeLookBlendTreeHash, 0.1f);
        else
            stateMachine.Animator.Play(freeLookBlendTreeHash);
    }


    public override void Tick(float deltaTime)
    {
        if (stateMachine.InputReader.IsAttacking)
        {
            stateMachine.SwitchState(new PlayerAttackingState(stateMachine, 0));
            return;
        }

        Vector3 direction = CalculateDirection();

        Move(direction * stateMachine.FreeLookSpeed, deltaTime);

        if (direction == Vector3.zero)
        {
            stateMachine.Animator.SetFloat(freeLookSpeedHash, 0, animTransitionSmoothingVal, deltaTime);
            return;
        }

        stateMachine.Animator.SetFloat(freeLookSpeedHash, 1, animTransitionSmoothingVal, deltaTime);
        FaceMovementDirection(direction, deltaTime);
    }


    public override void Exit()
    {
        stateMachine.InputReader.TargetEvent -= OnTarget;
        stateMachine.InputReader.JumpEvent -= OnJump;
    }

    Vector3 CalculateDirection()
    {
        Vector3 cameraForward = stateMachine.CameraTransfrom.forward;
        cameraForward.y = 0;
        cameraForward.Normalize();

        Vector3 cameraRight = stateMachine.CameraTransfrom.right;
        cameraRight.y = 0;
        cameraRight.Normalize();

        return cameraRight * stateMachine.InputReader.MovementValue.x
               + cameraForward * stateMachine.InputReader.MovementValue.y;
    }

    private void FaceMovementDirection(Vector3 movement, float deltaTime)
    {
        stateMachine.transform.rotation = Quaternion.RotateTowards(
                                              stateMachine.transform.rotation,
                                              Quaternion.LookRotation(movement),
                                              stateMachine.RotationSpeed * deltaTime); 
    }

    private void OnTarget()
    {
        if (!stateMachine.Targeter.SetTarget()) return;
        stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
    }
    private void OnJump()
    {
        stateMachine.SwitchState(new PlayerJumpState(stateMachine));    
    }
}
