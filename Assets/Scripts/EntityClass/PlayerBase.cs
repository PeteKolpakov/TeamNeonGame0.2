using System;
using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts.GameManager;

namespace Assets.Scripts.EntityClass
{
    public class PlayerBase : Entity
    {
        // base player class, should handle health and damage related functions
        PlayerStatManager player;
        FallBehaviour fallBehaviour;

        public delegate void RemoveArmorPoints();
        public static event RemoveArmorPoints removeArmor;

        private void Awake()
        {
            player = GetComponent<PlayerStatManager>();
            fallBehaviour = GetComponent<FallBehaviour>();
        }

        protected override void Die()
        {
            Destroy(gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            //ScenesManager.LoadLevel(SceneManager.GetActiveScene().buildIndex));
        }

        public override void TakeDamage(float damage, DamageType type)
        {
            if (!fallBehaviour.IsRespawnInvincible())
            {
                if(type == DamageType.Bullet)
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
                else if(type == DamageType.Fall)
                {
                    health -= damage;
                }
            }
        }

        
        
    }
}
