using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [field: SerializeField] public InputReader InputReader { get; private set; }
    [field: SerializeField] public CharacterController Controller { get; private set; }
    [field: SerializeField] public Animator Animator{ get; private set; }
    [field: SerializeField] public Targeter Targeter { get; private set; }
    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public  WeaponDamage WeaponDamage{ get; private set; }
    [field: SerializeField] public Ragdoll Ragdoll { get; private set; }
    [field: SerializeField] public LedgeDetector LedgeDetector { get; private set; }
    [field: SerializeField] public float FreeLookSpeed { get; private set; }
    [field: SerializeField] public float TargetLookSpeed { get; private set; }
    [field: SerializeField] public float RotationSpeed { get; private set; }
    [field: SerializeField] public float DodgeTime { get; private set; }
    [field: SerializeField] public float DodgeDistance { get; private set; }
    [field: SerializeField] public float JumpForce { get; private set; }
    [field: SerializeField] public Attack[] Attacks{ get; private set; }

    public Transform CameraTransfrom { get; private set; }

    private void Start()
    {
        CameraTransfrom = Camera.main.transform;
        SwitchState(new PlayerFreeLookState(this));
    }

    private void OnEnable()
    {
        Health.OnDamageTaken += HandleDamage;
        Health.OnDie += HandleDeath;
    }

    private void OnDisable()
    {
        Health.OnDamageTaken -= HandleDamage;
        Health.OnDie -= HandleDeath;
    }

    private void HandleDamage()
    {
        SwitchState(new PlayerImpactState(this));
    }
    private void HandleDeath()
    {
        SwitchState(new PlayerDeadState(this));
    }
}
