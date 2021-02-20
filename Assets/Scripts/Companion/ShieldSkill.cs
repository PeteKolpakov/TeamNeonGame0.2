using System.Collections;
using UnityEngine;
using Assets.Scripts.GameManager;
using Assets.Scripts.EntityClass;

namespace Companion
{
    class ShieldSkill : Skill
    {

        [SerializeField]
        private float _duration = 5f;

        [SerializeField]
        private GameObject _shieldSprite = default;

        private GameObject _shieldInstance = default;

        private bool _isActivated = false;

        public override void Activate()
        {
            // BUGGED FOR SOME REASON?!
            if (_isActivated)
            {
                _isActivated = false;
                return;
            }

            _isActivated = true;

            _shieldInstance = Instantiate(_shieldSprite, Vector3.zero, Quaternion.identity);

            // Add sprite of shield to player
            _shieldInstance.transform.SetParent(PlayerTracker.Instance.Player.transform);

            StartCoroutine(TurnOffShield());
        }

        public override void Deactivate()
        {
            _isActivated = false;
        }

        public override bool IsActive() => _isActivated;

        private IEnumerator TurnOffShield()
        {
            yield return new WaitForSeconds(_duration);
            Deactivate();

            Destroy(_shieldInstance);
        }
    }
}
