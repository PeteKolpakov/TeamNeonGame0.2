using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.GameManager;

namespace Assets.Scripts.Enemies
{
    public class EnemyBehaviour : MonoBehaviour
    {
        public float EnemySpeed;
        public float ShootingDistance = 18;
        public float StartTimeShots;

        private float _shootingDelay;
        private Vector2 _playerDistanceComparison;
        private Vector3 playerPos;


        [SerializeField]
        private GameObject _bulletPrefab = null;
        [SerializeField]
        private EnemyGunTest _weapon = null;

        private EnemyHealthBar _enemyHealthbar;
        private Entity _entity;

        public static Vector3 Direction;

        private void Awake()
        {
        }
        private void Start()
        {          
            _enemyHealthbar = GetComponent<EnemyHealthBar>();
            _entity = GetComponent<Entity>();
            _shootingDelay = StartTimeShots;
            _enemyHealthbar.SetMaxHealth(_entity.health);
        }
        private void Update()
        {
            _enemyHealthbar.SetHealth(_entity.health);
                Aim();

            if (Vector2.Distance(transform.position, _playerDistanceComparison) < ShootingDistance)
            {
                if(_shootingDelay <= 0)
                {
                    _shootingDelay = StartTimeShots;
                    Shoot();
                }
                else
                {
                    _shootingDelay -= Time.deltaTime;
                }
            } 
        }

        private void Aim()
        {
             playerPos = PlayerTracker.Instance.Player.transform.position;
            _playerDistanceComparison = playerPos;
            Direction = playerPos - transform.position;

            float angle = Mathf.Atan2(Direction.y, Direction.x);
            _weapon.transform.rotation = Quaternion.Euler(0f, 0f, angle * Mathf.Rad2Deg);

            Direction.z = 0;

            _weapon.transform.position = transform.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
        }
        private void Shoot()
        {
            _weapon.Attack();
        }
    }

}
