using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    
    int health;
    bool isDead = false;

    private void Start()
    {
        health = maxHealth;
    }

    public void DealDamage(int damageAmount)
    {
        if (isDead) return;
        
        health = Mathf.Max(health - damageAmount, 0);

        print($"{name}: {health}");

        if(health <= 0)
            isDead = true;
    }
}
