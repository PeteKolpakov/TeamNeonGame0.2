using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.EntityClass;

public class BossEyeball : Entity
{
    private EnemyHealthBar healthBar;

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
            gameObject.SetActive(false);

            if(gameObject.layer == 8){
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<StatsTracker>().EnemiesKilled++;
        }
    }
}
