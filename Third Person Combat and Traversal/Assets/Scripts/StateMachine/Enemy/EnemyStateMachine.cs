using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : StateMachine
{
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
    [field: SerializeField] public CharacterController Controller { get; private set; }
    [field: SerializeField] public NavMeshAgent Agent{ get; private set; }
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public Target Target{ get; private set; }
    [field: SerializeField] public Ragdoll Ragdoll{ get; private set; }
    [field: SerializeField] public WeaponDamage WeaponDamage{ get; private set; }
    [field: SerializeField] public float PlayerChasingRange { get; private set; }
    [field: SerializeField] public float MovementSpeed { get; private set; }
    [field: SerializeField] public float AttackRange { get; private set; }
    [field: SerializeField] public int AttackDamage { get; private set; }
    [field: SerializeField] public int AttackKnockback { get; private set; }
    public Health Player { get; private set; }

    private void Start()
    {
        Player = GameObject.FindWithTag("Player").GetComponent<Health>();
        Agent.updatePosition = false;
        Agent.updateRotation = false;

        SwitchState(new EnemyIdleState(this));
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
        SwitchState(new EnemyImpactState(this));
    }
    private void HandleDeath()
    {
        SwitchState(new EnemyDeadState(this));
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, PlayerChasingRange);
    }
}
