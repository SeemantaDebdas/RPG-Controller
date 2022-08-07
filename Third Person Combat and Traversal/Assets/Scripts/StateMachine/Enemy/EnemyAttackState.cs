using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    readonly int attackHash = Animator.StringToHash("Attack1");

    const float crossFadeDuration = 0.1f;

    public EnemyAttackState(EnemyStateMachine stateMachine) : base(stateMachine){}

    public override void Enter()
    {
        FaceTarget();
        stateMachine.WeaponDamage.SetAttack(stateMachine.AttackDamage, stateMachine.AttackKnockback);
        stateMachine.Animator.CrossFadeInFixedTime(attackHash, crossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        if(GetNormalizedTime(stateMachine.Animator) >= 1)
            stateMachine.SwitchState(new EnemyChaseState(stateMachine));
    }

    public override void Exit()
    { 
    }
}
