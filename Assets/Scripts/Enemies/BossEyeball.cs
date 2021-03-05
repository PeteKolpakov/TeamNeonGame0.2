using UnityEngine;

public class BossEyeball : Entity
{
    private EnemyHealthBar _healthBar;

    private void Start()
    {
        _healthBar = GetComponent<EnemyHealthBar>();

        _healthBar.SetMaxHealth(MaxHealth);
    }

    public override void Update()
    {
        base.Update();
        _healthBar.SetHealth(Health);
    }

    protected override void Die()
    {
        gameObject.SetActive(false);

        if (gameObject.layer == 8)
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<StatsTracker>().EnemiesKilled++;
        }
    }
}
