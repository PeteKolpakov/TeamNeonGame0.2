using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Companion
{
    abstract class Skill
    {
        private string _skillName;
        private GameObject _player;

        public Skill(GameObject player)
        {
            _player = player;
        }

        abstract public void Activate();

        public string GetSkillName() => _skillName;

        public void SetSkillName(string name) => _skillName = name;
    }
}
