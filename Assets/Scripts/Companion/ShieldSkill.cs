using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Companion
{
    class ShieldSkill : Skill
    {
        private void Start()
        {

        }

        private void Update()
        {

        }

        public override void Activate()
        {
            _isActivated = true;
            // Add sprite of shield to player
            // Make it inmune to damage from enemies


            _isActivated = false;
        }
    }
}
