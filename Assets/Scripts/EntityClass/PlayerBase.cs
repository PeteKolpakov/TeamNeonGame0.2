using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.EntityClass
{
    public class PlayerBase : Entity
    {
        // base player class, should handle health and damage related functions
        public PlayerStatManager player;

        public delegate void RemoveArmorPoints();
        public static event RemoveArmorPoints removeArmor;

        protected override void Die()
        {
            Destroy(gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public override void TakeDamage(float damage)
        {
            float _APBlock = player._currentArmorPoints * player._armorPointHealth;
            float damageTaken = damage - _APBlock;
            if (damageTaken < 0)
            {
                damageTaken = 0;
            }
            health -= damageTaken;

            if (damage > _APBlock)
            {
                if (removeArmor != null)
                {
                    removeArmor();
                }
            }
        }
    }
}
