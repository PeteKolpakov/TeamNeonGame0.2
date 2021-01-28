using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.CameraBehaviour
{
    class Chaser : MonoBehaviour
    {
        [SerializeField]
        private float _crawlSpeed = 2f;

        [SerializeField]
        private Transform _playerPosition;

        private Vector3 _direction;

        public bool startCrawl;

        private void Start()
        {
            startCrawl = true;
            _direction = new Vector3(0, _crawlSpeed, 0);
        }

        private void Update()
        {
            if (startCrawl == true)     
            {
                transform.Translate(_direction * Time.deltaTime);
            }
            if (_playerPosition.transform.position.y > transform.position.y)
            {
                transform.position = new Vector3(0, _playerPosition.position.y, 0);
            }
        }
    }
}
