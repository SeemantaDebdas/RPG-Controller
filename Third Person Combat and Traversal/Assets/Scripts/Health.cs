using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action OnDamageTaken;
    public event Action OnDie;

    [SerializeField] int maxHealth = 100;
    
    int health;
    bool isDead = false;
    bool isInvulnerable = false;

    public bool IsDead => isDead;

    private void Start()
    {
        health = maxHealth;
    }

    public void SetInvulnerable(bool isInvulnerable)
    {
        this.isInvulnerable = isInvulnerable;
    }

    public void DealDamage(int damageAmount)
    {
        if (isDead) return;
        if (isInvulnerable) return;

        OnDamageTaken?.Invoke();

        health = Mathf.Max(health - damageAmount, 0);
        if(health <= 0)
        {
            isDead = true;
            OnDie?.Invoke();   
        }
    }
}
