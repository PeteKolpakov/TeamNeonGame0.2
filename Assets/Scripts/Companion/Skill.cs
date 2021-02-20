using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Companion
{
    abstract class Skill : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("The name of this skill to be shown in the shop")]
        private string _skillName = default;
        [SerializeField]
        [Tooltip("The description of this skill to be shown in the shop")]
        private string _skillDescription = default;
        [SerializeField]
        [Tooltip("The cost of buying this skill in the shop")]
        private int _skillCost = 10;

        public bool IsPassiveSkill = false;

        public string GetSkillName() => _skillName;

        public void SetSkillName(string name) => _skillName = name;

        public string GetDescription() => _skillDescription;

        public int GetSkillCost() => _skillCost;

        public abstract void Activate();
        public abstract void Deactivate();

        public abstract bool IsActive();
    }
}
