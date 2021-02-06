using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Companion
{
    abstract class Skill : MonoBehaviour
    {
        [SerializeField]
        private string _skillName;
        [SerializeField]
        private string _skillDescription;

        [SerializeField]
        protected bool _isPassiveSkill = false;

        [SerializeField]
        protected GameObject _player;

        abstract public void Activate();

        public string GetSkillName() => _skillName;

        public void SetSkillName(string name) => _skillName = name;

        public string GetDescription() => _skillDescription;
    }
}
