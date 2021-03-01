using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.EntityClass;

public class BossLeftArm : MonoBehaviour
{
    private EnemyBase enemybase;
    private EnemyHealthBar healthBar;

    public Boss bossMain;

    private void Start() {
        enemybase = GetComponent<EnemyBase>();
        healthBar = GetComponent<EnemyHealthBar>();

        healthBar.SetMaxHealth(enemybase.maxHealth);
    }
    private void Update() {
        healthBar.SetHealth(enemybase.health);

        if(enemybase.health <= 0){
            bossMain.leftArmDead = true;
            Destroy(gameObject);
        }
    }

}
