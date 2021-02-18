using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageType
{
    Bullet,
    Melee,
    Fall
}

public class Entity : MonoBehaviour
{

    public float health;

    [SerializeField]
    public float _maxHealth;

    [SerializeField]
    GameObject currencyPrefab;
    [SerializeField]
    GameObject ammoPrefab;

    public int currencyDropChance = 30;
    public int ammoDropChance = 30;
    private int getFuckedChance = 40;

    private bool _canDrop = true;

    private void Start()
    {
        health = _maxHealth;

        getFuckedChance = 100 - currencyDropChance - ammoDropChance;

        if (System.Math.Abs(getFuckedChance) + currencyDropChance + ammoDropChance != 100)
        {
            Debug.Log("Drop percentages do not equal 100. Change the values and try again, darling");
            _canDrop = false;
        }
    }

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
        Drop();
        Destroy(gameObject);
    }

    public void Drop()
    {
        if (_canDrop == true)
        {
            float roll = Random.Range(1, 100f);

            if (roll <= currencyDropChance)
            {
                Debug.Log("You get MONEY!");
                Instantiate(currencyPrefab, transform.position, Quaternion.identity);
            }
            else if (roll >= currencyDropChance + 1 && roll <= currencyDropChance + ammoDropChance)
            {
                Debug.Log("You get AMMO!");
                Instantiate(ammoPrefab, transform.position, Quaternion.identity);
            }
            else if (roll >= currencyDropChance + ammoDropChance + 1)
            {
                Debug.Log("You get FUCKED!");

                // GET FUCKED
            }
        }
    }
}
