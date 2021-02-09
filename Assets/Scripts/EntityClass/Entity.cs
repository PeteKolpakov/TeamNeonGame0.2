using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{

    public int health;

    [SerializeField]
    private int _maxHealth;

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

    public int GetHealth()
    {
        return health;
    }

    public void Heal(int healAmount)
    {
        health += healAmount;
        if (health > _maxHealth) health = _maxHealth;
    }

    public virtual void TakeDamage(int damage)
    {
        // TODO - take armor ponts into account
        health -= damage;
    }

    public void SetNewMaxHealth(int newMax)
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
