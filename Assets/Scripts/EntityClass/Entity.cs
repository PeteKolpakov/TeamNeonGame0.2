using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageType
{
    Bullet,
    Fall
}

public class Entity : MonoBehaviour
{

    public float health;

    [SerializeField]
    public float _maxHealth;

    private void Update()
    {
        if (health<= 0)
        {
            Die();
        }
    }
    public void Initialize() // to be called at the beginning of a lvl
    {
        health = _maxHealth;
    }

    public float GetHealth()
    {
        return health;
    }

    public void Heal(float healAmount)
    {
        health += healAmount;
        if (health > _maxHealth) health = _maxHealth;
    }

    public virtual void TakeDamage(float damage, DamageType type)
    {
        health -= damage;
    }

    public void SetNewMaxHealth(float newMax)
    {
        _maxHealth = newMax;
        if(newMax <= 0)
        {
            Debug.LogError("WARNING: Max HP must be greater than zero!");
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
