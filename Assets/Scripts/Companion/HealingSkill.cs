using System.Collections;
using UnityEngine;
using Assets.Scripts.GameManager;
using Assets.Scripts.EntityClass;

namespace Companion
{
    class HealingSkill : Skill
    {
        private Coroutine _healingCoroutine;

        [SerializeField]
        [Tooltip("Amount of health to restore the player")]
        private int _healAmount = 10;

        [SerializeField]
        [Tooltip("Time to wait to heal again the player")]
        private float _healCooldown = 2f; // Time in seconds

        public override void Activate()
        {
            _isActivated = true;
            _healingCoroutine = StartCoroutine(HealPlayer());
        }

        private IEnumerator HealPlayer()
        {
            while (_isActivated)
            {
                yield return new WaitForSeconds(_healCooldown);
                int willHeal = Random.Range(0, 2);

                // if it's 0, it won't heal. 
                // If it's 1, player will be healed.
                if (willHeal == 1)
                {
                    PlayerTracker.Instance.Player.GetComponent<PlayerBase>().Heal(_healAmount);
                }

            }
            
        }

    }
}
