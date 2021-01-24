using System.Collections;
using UnityEngine;

namespace Companion
{
    class HealingSkill : Skill
    {
        private Entity _playerEntity;
        
        public HealingSkill(GameObject player, Entity entity) : base(player)
        {
            _playerEntity = entity;
        }

        public override void Activate()
        {
        }

        private IEnumerator HealPlayer()
        {
            yield return new WaitForSeconds(30f);
            _playerEntity.Heal(10);
        }

    }
}
