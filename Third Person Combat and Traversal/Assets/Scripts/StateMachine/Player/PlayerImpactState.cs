using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerImpactState : PlayerBaseState
{
    readonly int impactHash = Animator.StringToHash("Impact");

    const float crossFadeDuration = 0.1f;

    float impactTime = 1f;

    public PlayerImpactState(PlayerStateMachine stateMachine) : base(stateMachine){}

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(impactHash, crossFadeDuration);
    }
    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        impactTime -= deltaTime;
        if(impactTime < 0f)
            ReturnToLocomotion();
    }
    public override void Exit()
    {
    }
}
