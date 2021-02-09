using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    class EnemyGunTest : MonoBehaviour
    {
        [SerializeField]
        private Transform _spawnPoint;
        [SerializeField]
        private GameObject _bulletPrefab;

        public void Attack()
        {
            Instantiate(_bulletPrefab, _spawnPoint.position, transform.rotation);
        }
    }
}
