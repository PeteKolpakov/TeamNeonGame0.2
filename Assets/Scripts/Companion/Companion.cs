using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Companion
{
    class Companion : MonoBehaviour
    {
        [SerializeField]
        private Transform _playerTransform;  // Reference to transform component of the player gameobject

        [SerializeField]
        private float _petMovementSpeed = 0.5f;

        [SerializeField]
        private float _petOffSet = 1.2f;  // The distance to keep from the player

        private void Start()
        {
            if (_playerTransform == null)
            {
                Debug.LogError("Player Transform is missing in Companion script");
            }
        }

        private void Update()
        {
            if (_playerTransform != null)
            {
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
            }
        }

        public void ActivateSkill(int skill)
        {
            //TODO: Add an example skill to showcase
        }
    }
}
