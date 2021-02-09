using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.GameManager;

namespace Companion
{
    class Companion : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("The speed the companion will follow the player with")]
        private float _petMovementSpeed = 2f;

        [SerializeField]
        [Tooltip("The distance to keep from the player")]
        private float _petOffSet = 1.2f;

        [SerializeField]
        [Tooltip("The name in the Input Manager given to the key used to activate the current companion active skill")]
        private string _companionSkillButtonName = "CompanionSkill";

        [SerializeField]
        [Tooltip("The reference to the current passive skill")]
        private Skill _passiveSkill;

        [SerializeField]
        [Tooltip("The reference to the current active skill")]
        private Skill _activeSkill;

        private Transform _playerTransform = default;

        private void Start()
        {
            _playerTransform = PlayerTracker.Instance.Player.GetComponent<Transform>();
        }

        private void Update()
        {
            // Companion movement
            if (Vector3.Distance(transform.position, _playerTransform.position) > _petOffSet)
            {
                // Follow the player
                transform.position = Vector3.Lerp(transform.position, _playerTransform.position, _petMovementSpeed * Time.deltaTime);
            }
            else
            {
                // Rotate around the player if it's not moving
                transform.RotateAround(_playerTransform.position, new Vector3(0, 0, 1f), _petMovementSpeed);
            }

            // This can be moved into the player controller. It's used to activate the active skill of the companion.
            // Works by having a button in the InputManager of the project to assign to the CompanionSkill.
            if (Input.GetButtonDown(_companionSkillButtonName))  
            {
                ActivateSkill();
            }

            // This activates the passive skill if not activated already
            if (!_passiveSkill.IsActivated) _passiveSkill.Activate();
        }

        public void ActivateSkill()
        {
            //TODO: Add an example skill to showcase
        }

        public void ChangeActiveSkill(Skill newActiveSkill)
        {
            _activeSkill.DeactivateSkill();
            _activeSkill = newActiveSkill;
        }

        public void ChangePassiveSkill(Skill newPassiveSkill)
        {
            _passiveSkill.DeactivateSkill();
            _passiveSkill = newPassiveSkill;
        }
    }
}
