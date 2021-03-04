using Assets.Scripts.Audio;
using UnityEngine;

public enum DamageType
{
    Bullet,
    Fall
}
[RequireComponent(typeof(AudioEventTrigger))]
public class Entity : MonoBehaviour
{
    public int Health;

    public int MaxHealth;

    [SerializeField]
    public GameObject Explosion;
    [SerializeField]
    GameObject currencyPrefab;
    [SerializeField]
    GameObject ammoPrefab;

    private AudioEventTrigger _deathAudio;

    public int CurrencyDropPercent = 30;
    public int AmmodropPercent = 30;
    public int NothingPercent = 40;

    private bool _canDrop = true;
    public bool CanTakeDamage = true;

    private void Start()
    {
        Health = MaxHealth;

        NothingPercent = 100 - CurrencyDropPercent - AmmodropPercent;

        if (System.Math.Abs(NothingPercent) + CurrencyDropPercent + AmmodropPercent != 100)
        {
            _canDrop = false;
        }
    }

    private void Awake()
    {
        _deathAudio = GetComponent<AudioEventTrigger>();
    }

    public virtual void Update()
    {
        if (Health <= 0)
        {
            Die();
        }
    }
    public void Initialize() // to be called at the beginning of a lvl
    {
        Health = MaxHealth;
    }

    public float GetHealth()
    {
        return Health;
    }

    public void Heal(int healAmount)
    {
        Health += healAmount;
        if (Health > MaxHealth) Health = MaxHealth;
    }

    public virtual void TakeDamage(int damage, DamageType type)
    {
        if (CanTakeDamage == true)
        {
            Health -= damage;
        }
    }

    public void SetNewMaxHealth(int newMax)
    {
        MaxHealth = newMax;
        if (newMax <= 0)
        {
           
        }
    }

    protected virtual void Die()
    {
        _deathAudio.PlaySound();
        if (Explosion != null)
        {
            Instantiate(Explosion, transform.position, Quaternion.identity);
        }
        Drop();

        // If the entity that dies is an enemy, we add 1 to the counter
        if (gameObject.layer == 8)
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<StatsTracker>().EnemiesKilled++;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatManager>().AddScore(25);
        }

        Destroy(gameObject);
    }

    public void Drop()
    {
        if (_canDrop == true && currencyPrefab != null)
        {
            float roll = Random.Range(1, 100f);

            if (roll <= CurrencyDropPercent)
            {
                Instantiate(currencyPrefab, transform.position, Quaternion.identity);
            }
            else if (roll >= CurrencyDropPercent + 1 && roll <= CurrencyDropPercent + AmmodropPercent)
            {
                Instantiate(ammoPrefab, transform.position, Quaternion.identity);
            }
            else if (roll >= CurrencyDropPercent + AmmodropPercent + 1)
            {
                return;
            }
        }
    }

}
