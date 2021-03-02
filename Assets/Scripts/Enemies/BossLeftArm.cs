using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.EntityClass;

public class BossLeftArm : Entity
{
    private EnemyHealthBar healthBar;

    public Boss bossMain;
    public GameObject firepoint;

    private void Start() {
        healthBar = GetComponent<EnemyHealthBar>();

        healthBar.SetMaxHealth(maxHealth);
    }
    public override void Update() {
        base.Update();
        healthBar.SetHealth(health);
    }

    protected override void Die()
    {
        bossMain.leftArmDead = true;
            Destroy(gameObject);

            if (_explosion != null)
        {
            Instantiate(_explosion, firepoint.transform.position, Quaternion.identity);
        }

            if(gameObject.layer == 8){
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<StatsTracker>().EnemiesKilled++;
        }
    }

}
