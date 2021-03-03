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

        private PlayerAudio _audio;

        private void Awake()
        {
            player = GetComponent<PlayerStatManager>();
            fallBehaviour = GetComponent<FallBehaviour>();
            _audio = GetComponent<PlayerAudio>();
        }

        protected override void Die()
        {
            Destroy(gameObject);
            //StartCoroutine(LoadLevel(SceneManager.GetActiveScene()));
            ScenesManager.GoToScene(SceneManager.GetActiveScene().buildIndex);
        }

        public override void TakeDamage(int damage, DamageType type)
        {
            
            if (!fallBehaviour.IsRespawnInvincible() && canTakeDamage == true)
            {
                if(type == DamageType.Bullet)
                {
                    _audio.PlaySFX(_audio._takeDamageSFX);
                    health -= damage;

                }
                else if(type == DamageType.Fall)
                {
                    _audio.PlaySFX(_audio._fallSFX);
                    health -= damage;
                }
            }
        }

        
        
    }
}
