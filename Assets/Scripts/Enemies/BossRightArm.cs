using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.EntityClass;


public class BossRightArm : Entity
{
    private EnemyHealthBar healthBar;
    public Boss bossMain;

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
        bossMain.rightArmDead = true;
            Destroy(gameObject);

            if(gameObject.layer == 8){
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<StatsTracker>().EnemiesKilled++;
        }
    }
}
