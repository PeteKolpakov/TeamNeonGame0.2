using System.Collections;
using UnityEngine;

namespace Companion
{
    class HealingSkill : Skill
    {
        private Entity _playerEntity;

        private Coroutine _healingCoroutine;

        [SerializeField]
        [Tooltip("Amount of health to restore the player")]
        private int _healAmount = 10;

        [SerializeField]
        [Tooltip("Time to wait to heal again the player")]
        private float _healCooldown = 10f; // Time in seconds

        private void Start()
        {
            _playerEntity = _player.GetComponent<Entity>();
        }

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
                    _playerEntity.Heal(_healAmount);
                    Debug.Log("Healing Player");
                }

            }
            
        }

    }
}
