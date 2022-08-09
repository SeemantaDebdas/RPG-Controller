using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackingState : PlayerBaseState
{
    Attack currentAttack = null;
    bool appliedForce = false;
    public PlayerAttackingState(PlayerStateMachine stateMachine, int attackID) : base(stateMachine)
    {
        currentAttack = stateMachine.Attacks[attackID];
    }

    public override void Enter()
    {
        stateMachine.WeaponDamage.SetAttack(currentAttack.Damage, currentAttack.Knockback);
        stateMachine.Animator.CrossFadeInFixedTime(currentAttack.AnimationName, currentAttack.TransitionDuration);
    }
    public override void Tick(float deltaTime)
    {
        FaceTarget();
        Move(deltaTime);
        float normalizedTime = GetNormalizedTime(stateMachine.Animator, "Attack");

        if(normalizedTime < 1)
        {
            if (stateMachine.InputReader.IsAttacking)
            {
                TryComboAttack(normalizedTime);
                TryApplyForce(normalizedTime);
            }
        }
        else
        {
            ReturnToLocomotion();
        }
    }

    public override void Exit()
    {
    }

    void TryComboAttack(float normalizedTime)
    {
        if (currentAttack.ComboStateIndex == -1) return;
        if (normalizedTime < currentAttack.ComboAttackTime) return;

        stateMachine.SwitchState
        (
            new PlayerAttackingState
            (
                stateMachine,
                currentAttack.ComboStateIndex
            )
        );
    }

    void TryApplyForce(float normalizedTime)
    {
        if(normalizedTime < currentAttack.ForceTime) return;

        if (appliedForce) return;
        stateMachine.ForceReceiver.AddForce(stateMachine.transform.forward * currentAttack.Force);
        appliedForce = true;
    }
}
