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

    [SerializeField]
    GameObject currencyPrefab;
    [SerializeField]
    GameObject ammoPrefab;

    public int currencyDropPercent = 30;
    public int ammodropPercent = 30;
    private int nothingPercent = 40;

    private bool _canDrop = true;

    private void Start()
    {
        health = _maxHealth;

        nothingPercent = 100 - currencyDropPercent - ammodropPercent;

        if (System.Math.Abs(nothingPercent) + currencyDropPercent + ammodropPercent != 100)
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

            if (roll <= currencyDropPercent)
            {
                Debug.Log("You get MONEY!");
                Instantiate(currencyPrefab, transform.position, Quaternion.identity);
            }
            else if (roll >= currencyDropPercent + 1 && roll <= currencyDropPercent + ammodropPercent)
            {
                Debug.Log("You get AMMO!");
                Instantiate(ammoPrefab, transform.position, Quaternion.identity);
            }
            else if (roll >= currencyDropPercent + ammodropPercent + 1)
            {
                Debug.Log("Nothing");

                // GET FUCKED
            }
        }
    }
}
