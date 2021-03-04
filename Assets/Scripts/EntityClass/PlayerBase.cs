using System;
using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts.GameManager;
using SpriteGlow;

namespace Assets.Scripts.EntityClass
{
    public class PlayerBase : Entity
    {
        // base player class, should handle health and damage related functions
        PlayerStatManager player;
        FallBehaviour fallBehaviour;
        [SerializeField]
        private GameObject _playerDeathAnim;

        public delegate void RemoveArmorPoints();
        public static event RemoveArmorPoints removeArmor;

        private PlayerAudio _audio;
        private Color _originalColor;

        private void Awake()
        {
            player = GetComponent<PlayerStatManager>();
            fallBehaviour = GetComponent<FallBehaviour>();
            _audio = GetComponent<PlayerAudio>();
            _originalColor = GetComponent<SpriteRenderer>().color;
        }

        protected override void Die()
        {
            Instantiate(_playerDeathAnim, transform.position, Quaternion.identity);
            Debug.Log("Player base");
            StartCoroutine(DelayDeath());
            
            Destroy(gameObject);
            ScenesManager.GoToScene(SceneManager.GetActiveScene().buildIndex);
            player.RemoveScore(30);
        }

        public override void TakeDamage(int damage, DamageType type)
        {
            
            if (!fallBehaviour.IsRespawnInvincible() && canTakeDamage == true)
            {
                if(type == DamageType.Bullet)
                {
                    _audio.PlaySFX(_audio._takeDamageSFX);
                    health -= damage;
                    StartCoroutine(HurtColorChange());
                }
                else if(type == DamageType.Fall)
                {
                    _audio.PlaySFX(_audio._fallSFX);
                    health -= damage;
                }
            }
        }
        public IEnumerator DelayDeath()
        {
            yield return new WaitForSeconds(1.5f);
        }
        public IEnumerator HurtColorChange()
        {
            GetComponent<SpriteRenderer>().color = new Color(253, 15, 20);
            yield return new WaitForSeconds(0.06f);
            GetComponent<SpriteRenderer>().color = _originalColor;
        }
    }
}
