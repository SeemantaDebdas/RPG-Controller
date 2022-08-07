using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState : State
{
    protected EnemyStateMachine stateMachine;
    public EnemyBaseState(EnemyStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    protected void Move(Vector3 motion, float deltaTime)
    {
        Vector3 direction = motion + stateMachine.ForceReceiver.Movement;
        stateMachine.Controller.Move(deltaTime * direction);
    }

    protected void Move(float deltaTime)
    {
        Move(Vector3.zero, deltaTime);
    }

    protected bool IsInRange()
    {
        if(stateMachine.Player.IsDead)return false;

        float chaseRange = stateMachine.PlayerChasingRange;
        float distanceToPlayer = Vector3.Distance(stateMachine.transform.position, stateMachine.Player.transform.position);

        if (distanceToPlayer < chaseRange)
            return true;

        return false;
    }

    protected void FaceTarget()
    {
        if (stateMachine.Player == null) return;

        Vector3 directionToTarget = stateMachine.Player.transform.position - stateMachine.transform.position;
        directionToTarget.y = 0;

        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);

        stateMachine.transform.rotation = lookRotation;
    }
}
