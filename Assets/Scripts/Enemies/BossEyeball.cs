using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.EntityClass;

public class BossEyeball : MonoBehaviour
{
   private EnemyBase enemybase;
    private EnemyHealthBar healthBar;

    private void Start() {
        enemybase = GetComponent<EnemyBase>();
        healthBar = GetComponent<EnemyHealthBar>();

        healthBar.SetMaxHealth(enemybase.maxHealth);
    }
    private void Update() {
        healthBar.SetHealth(enemybase.health);
    }
}
