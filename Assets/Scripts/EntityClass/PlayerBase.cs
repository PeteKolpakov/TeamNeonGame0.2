using System;
using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts.GameManager;
using Assets.Scripts.Audio;

namespace Assets.Scripts.EntityClass
{
    [RequireComponent(typeof(PlayerAudio))]
    public class PlayerBase : Entity
    {
        // base player class, should handle health and damage related functions
        PlayerStatManager player;
        FallBehaviour fallBehaviour;

        private PlayerAudio _audio;

        public delegate void RemoveArmorPoints();
        public static event RemoveArmorPoints removeArmor;

        private void Awake()
        {
            player = GetComponent<PlayerStatManager>();
            fallBehaviour = GetComponent<FallBehaviour>();
            _audio = GetComponent<PlayerAudio>();
        }

        protected override void Die()
        {
            _audio.TriggerAudio(_audio._deathSFX);
            Destroy(gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            //ScenesManager.LoadLevel(SceneManager.GetActiveScene().buildIndex));
        }

        public override void TakeDamage(int damage, DamageType type)
        {
            if (!fallBehaviour.IsRespawnInvincible())
            {
                if(type == DamageType.Bullet)
                {
                    _audio.TriggerAudio(_audio._hurtSFX);
                    health -= damage;

                }
                else if(type == DamageType.Fall)
                {
                    _audio.TriggerAudio(_audio._fallSFX);
                    health -= damage;
                }
            }
        }

        
        
    }
}
