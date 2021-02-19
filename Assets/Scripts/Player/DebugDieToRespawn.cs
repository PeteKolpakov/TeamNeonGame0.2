using Assets.Scripts.EntityClass;
using UnityEngine;

namespace Assets.Scripts.Player
{
    class DebugDieToRespawn : MonoBehaviour
    {
        private PlayerBase _player;
        private void Start()
        {
            if (_player == null)
                _player = GetComponent<PlayerBase>();
            else _player = default;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F8))
                _player.TakeDamage(float.MaxValue, DamageType.Fall);
        }
    }
}
