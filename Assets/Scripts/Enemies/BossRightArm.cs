using UnityEngine;


public class BossRightArm : Entity
{
    private EnemyHealthBar _healthBar;
    public Boss BossMain;
    public GameObject Firepoint;

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
        BossMain.RightArmDead = true;
        Destroy(gameObject);
        if (Explosion != null)
        {
            Instantiate(Explosion, Firepoint.transform.position, Quaternion.identity);
        }

        if (gameObject.layer == 8)
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<StatsTracker>().EnemiesKilled++;
        }
    }
}
