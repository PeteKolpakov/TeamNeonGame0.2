using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.GameManager;
using Assets.Scripts.Audio;

public enum DamageType
{
    Bullet,
    Fall
}
[RequireComponent(typeof(AudioEventTrigger))]
public class Entity : MonoBehaviour
{
    public float health;

    [SerializeField]
    public float maxHealth;

    [SerializeField]
    public GameObject _explosion;
    [SerializeField]
    GameObject currencyPrefab;
    [SerializeField]
    GameObject ammoPrefab;

    private AudioEventTrigger _deathAudio;

    public int currencyDropPercent = 30;
    public int ammodropPercent = 30;
    private int nothingPercent = 40;

    private bool _canDrop = true;
    public bool canTakeDamage = true;

    private void Start()
    {
        health = maxHealth;

        nothingPercent = 100 - currencyDropPercent - ammodropPercent;

        if (System.Math.Abs(nothingPercent) + currencyDropPercent + ammodropPercent != 100)
        {
            Debug.Log("Drop percentages do not equal 100. Change the values and try again, darling");
            _canDrop = false;
        }
    }

    private void Awake()
    {
        _deathAudio = GetComponent<AudioEventTrigger>();
    }

    public virtual void Update()
    {
        if (health<= 0)
        {
            Die();
        }
    }
    public void Initialize() // to be called at the beginning of a lvl
    {
        health = maxHealth;
    }

    public float GetHealth()
    {
        return health;
    }

    public void Heal(float healAmount)
    {
        health += healAmount;
        if (health > maxHealth) health = maxHealth;
    }

    public virtual void TakeDamage(int damage, DamageType type)
    {
        if(canTakeDamage == true){
            int randomDamage = Random.Range(-3,4);
            damage += randomDamage;

            health -= damage;
        }
    }

    public void SetNewMaxHealth(float newMax)
    {
        maxHealth = newMax;
        if(newMax <= 0)
        {
            Debug.LogError("WARNING: Max HP must be greater than zero!");
        }
    }

    protected virtual void Die()
    {
        _deathAudio.PlaySound();
        Debug.Log("Entity class DIE method");
        if (_explosion != null)
        {
            Instantiate(_explosion, transform.position, Quaternion.identity);
        }
        Drop();

        // If the entity that dies is an enemy, we add 1 to the counter
        if(gameObject.layer == 8){
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<StatsTracker>().EnemiesKilled++;
        }

        Destroy(gameObject);
    
  
    }

    public void Drop()
    {
        if (_canDrop == true && currencyPrefab != null)
        {
            float roll = Random.Range(1, 100f);

            if (roll <= currencyDropPercent)
            {
                Instantiate(currencyPrefab, transform.position, Quaternion.identity);
            }
            else if (roll >= currencyDropPercent + 1 && roll <= currencyDropPercent + ammodropPercent)
            {
                Instantiate(ammoPrefab, transform.position, Quaternion.identity);
            }
            else if (roll >= currencyDropPercent + ammodropPercent + 1)
            {
                return;
            }
        }
    }

}
