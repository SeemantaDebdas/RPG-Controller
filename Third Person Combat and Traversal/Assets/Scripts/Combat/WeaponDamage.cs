using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    List<Collider> alreadyCollidedWith = new();
    int damage = 0;

    private void OnEnable()
    {
        alreadyCollidedWith.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")){ return; }

        if (alreadyCollidedWith.Contains(other)) return;
        alreadyCollidedWith.Add(other);

        if (other.TryGetComponent(out Health health))
            health.DealDamage(damage);
    }

    public void SetAttack(int damage)
    {
        this.damage = damage;
    }
}
