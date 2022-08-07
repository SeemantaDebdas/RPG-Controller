using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] private Collider myCollider;

    List<Collider> alreadyCollidedWith = new();
    int damage = 0;
    int knockback = 0;

    private void OnEnable()
    {
        alreadyCollidedWith.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == myCollider){ return; }

        if (alreadyCollidedWith.Contains(other)) return;
        alreadyCollidedWith.Add(other);

        if (other.TryGetComponent(out Health health))
        {
            health.DealDamage(damage);
        }
        if(other.TryGetComponent(out ForceReceiver forceReceiver))
        {
            Vector3 direction = (other.transform.position - myCollider.transform.position).normalized;
            forceReceiver.AddForce(knockback * direction);
        }
    }

    public void SetAttack(int damage, int knockback)
    {
        this.damage = damage;
        this.knockback = knockback;
    }
}
