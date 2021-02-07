using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Companion
{
    abstract class Skill : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("The name of this skill to be shown in the shop")]
        private string _skillName;
        [SerializeField]
        [Tooltip("The description of this skill to be shown in the shop")]
        private string _skillDescription;

        public bool IsPassiveSkill;

        [SerializeField]
        [Tooltip("Reference to the player gameObject")]
        protected GameObject _player;

        protected bool _isActivated;

        public bool IsActivated => _isActivated;

        abstract public void Activate();

        public string GetSkillName() => _skillName;

        public void SetSkillName(string name) => _skillName = name;

        public string GetDescription() => _skillDescription;

        public void DeactivateSkill ()
        {
            _isActivated = false;
        }
    }
}
