using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{
    readonly int locomotionBlendTreeHash = Animator.StringToHash("LocomotionBlendTree");
    readonly int speedHash = Animator.StringToHash("Speed");

    const float crossFadeDuration = 0.1f;
    const float animDampTime = 0.1f;

    public EnemyChaseState(EnemyStateMachine stateMachine) : base(stateMachine){}

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(locomotionBlendTreeHash, crossFadeDuration);
    }
    public override void Tick(float deltaTime)
    {
        if (!IsInRange())
        {
            stateMachine.SwitchState(new EnemyIdleState(stateMachine));
            return;
        }
        if (IsInAttackRange())
        {
            stateMachine.SwitchState(new EnemyAttackState(stateMachine));
            return;
        }

        FaceTarget();
        MoveToPlayer(deltaTime);

        stateMachine.Animator.SetFloat(speedHash, 1, animDampTime, deltaTime);
    }
    public override void Exit()
    {
        stateMachine.Agent.ResetPath();
        stateMachine.Agent.velocity = Vector3.zero;
    }
    private void MoveToPlayer(float deltaTime)
    {
        if (stateMachine.Agent.isOnNavMesh)
        {
            stateMachine.Agent.destination = stateMachine.Player.transform.position;
            Move(stateMachine.Agent.desiredVelocity.normalized * stateMachine.MovementSpeed, deltaTime);
        }
        stateMachine.Agent.velocity = stateMachine.Controller.velocity;
    }
    private bool IsInAttackRange()
    {
        if (stateMachine.Player.IsDead) return false;

        return Vector3.Distance(stateMachine.Player.transform.position, stateMachine.transform.position) 
                < stateMachine.AttackRange;
    }
}
