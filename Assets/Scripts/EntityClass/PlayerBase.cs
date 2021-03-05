using Assets.Scripts.GameManager;
using Assets.Scripts.Player;
using SpriteGlow;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        private float _oldBrightness;



        private void Awake()
        {
            player = GetComponent<PlayerStatManager>();
            fallBehaviour = GetComponent<FallBehaviour>();
            _audio = GetComponent<PlayerAudio>();
            glow = GetComponent<SpriteGlowEffect>();
            _originalColor = glow.GlowColor;
            _oldBrightness = glow.GlowBrightness;
        }

        protected override void Die()
        {
            Instantiate(_playerDeathAnim, transform.position, Quaternion.identity);
            StatsTracker stats = GameObject.FindGameObjectWithTag("GameManager").GetComponent<StatsTracker>();
            stats.DeathCount++;
            StartCoroutine(DelayDeath());

            
            //Destroy(gameObject);
            ScenesManager.GoToScene(SceneManager.GetActiveScene().buildIndex);
            player.RemoveScore(30);
        }

        public override void TakeDamage(int damage, DamageType type)
        {

            if (!fallBehaviour.IsRespawnInvincible() && CanTakeDamage == true)
            {
                if (type == DamageType.Bullet)
                {
                    _audio.PlaySFX(_audio._takeDamageSFX);
                    Health -= damage;
                    if (glow.GlowColor == new Color(0, 1, 0, 255))
                    {
                        StartCoroutine(HurtColorChangeWhileBuffed());
                    }
                    else
                    {
                        StartCoroutine(HurtColorChange());
                    }
                }
                else if (type == DamageType.Fall)
                {
                    _audio.PlaySFX(_audio._fallSFX);
                    Health -= damage;
                }
            }
        }
        public IEnumerator DelayDeath()
        {
            yield return new WaitForSeconds(1.5f);
        }
        public IEnumerator HurtColorChange()
        {
            glow.GlowBrightness = 0.5f;
            glow.GlowColor = new Color(253, 15, 20);
            yield return new WaitForSeconds(0.1f);
            glow.GlowColor = _originalColor;
            glow.GlowBrightness = _oldBrightness;
        }
        public IEnumerator HurtColorChangeWhileBuffed()
        {
            glow.GlowColor = new Color(253, 15, 20);
            yield return new WaitForSeconds(0.1f);
            glow.GlowColor = new Color(0, 1, 0, 255);
        }
    }
}
