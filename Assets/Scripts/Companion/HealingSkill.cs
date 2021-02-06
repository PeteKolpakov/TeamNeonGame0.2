using System.Collections;
using UnityEngine;

namespace Companion
{
    class HealingSkill : Skill
    {
        private Entity _playerEntity;

        private Coroutine _healingCoroutine;

        bool _isPassiveSkill = true;

        private void Start()
        {
            _playerEntity = _player.GetComponent<Entity>();
        }

        public override void Activate()
        {
            _healingCoroutine = StartCoroutine(HealPlayer());
        }

        private IEnumerator HealPlayer()
        {
            yield return new WaitForSeconds(30f);
            _playerEntity.Heal(Random.Range(0, 10));
        }

    }
}
