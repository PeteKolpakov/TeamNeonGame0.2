using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    class ProjectileMovement : MonoBehaviour
    {
        [SerializeField]
        private float _bulletSpeed;
        private void Start()
        {

        }

        private void Update()
        {
            Move(transform.right);

        }

        private void Move(Vector3 direction)
        {
            transform.Translate(direction.normalized * Time.deltaTime * _bulletSpeed, Space.World);
        }

    }
}