using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.CameraBehaviour
{
    class Chaser : MonoBehaviour
    {
        [SerializeField] private float _crawlSpeed = 2f;
        [SerializeField] private Transform _playerPosition;

        //[SerializeField][Range(0.1f, 3f)]    private float _maxSpeedUp = 2;
        //[SerializeField][Range(0.01f, 0.5f)] private float _speedUpIncrement = 0.02f;

        private float _speedUp = 0f;
        private bool _canMove;

        private void Start()
        {
            _speedUp = 0;
            SetCanMove(true);
        }

        private void Update()
        {     
            TrackPlayer();
        }

        private void TrackPlayer()
        {
            Vector3 direction = new Vector3(0, _crawlSpeed * (_speedUp + 1), 0);
            if (_canMove == true)
            {
                transform.Translate(direction * Time.deltaTime);
            }
            if (_playerPosition.transform.position.y > transform.position.y)
            {
                transform.position = new Vector3(0, _playerPosition.position.y, 0);
                //IncrementSpeed();
            }
        }

        //private void IncrementSpeed()
        //{
        //    if (_speedUp <= _maxSpeedUp)
        //    {
        //        _speedUp += _speedUpIncrement;
        //    }
        //}

        public void SetCanMove(bool condition)
        {
            _canMove = condition;
        }

        public void SetSpeed(float speed)
        {
            _crawlSpeed = speed;
        }
    }
}
