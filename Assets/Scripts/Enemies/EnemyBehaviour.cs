using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.GameManager;

namespace Assets.Scripts.Enemies
{
    public class EnemyBehaviour : MonoBehaviour
    {
        public float _enemySpeed;
    
        public float _stopingDistance;
    
        public float _retreatdistance;

        public float _startTimeShots;

        private float _shootingDelay;

        private Transform playerPos;

        [SerializeField]
        private GameObject _bulletPrefab;
        [SerializeField]
        private EnemyGunTest _weapon;

        public static Vector3 Direction;

        private void Awake()
        {
        }
        private void Start()
        {          
            _shootingDelay = _startTimeShots;
        }
        private void Update()
        {
            //CalculateDistance(); //can put the moveTowards method on a Couroutine, to have it not calculate it each frame, want some feedback.Or with NavhMesh, also not using physics, so much questions 
            Aim();

            if(_shootingDelay <= 0)
            {
                _shootingDelay = _startTimeShots;
                Shoot();
            }
            else
            {
                _shootingDelay -= Time.deltaTime;
            }
        }

        /**private void CalculateDistance()
        {
            if(Vector2.Distance(transform.position, playerPos.position) > _stopingDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, playerPos.position, _enemySpeed * Time.deltaTime);

            }else if(Vector2.Distance(transform.position, playerPos.position) < _stopingDistance && Vector2.Distance(transform.position, playerPos.position) > _retreatdistance){
                transform.position = this.transform.position;
            }else if(Vector2.Distance(transform.position, playerPos.position) < _retreatdistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, playerPos.position, -_enemySpeed * Time.deltaTime);
            }
        }**/
        private void Aim()
        {
            Vector3 playerPos = PlayerTracker.Instance.Player.transform.position;
            Direction = playerPos - transform.position;

            float angle = Mathf.Atan2(Direction.y, Direction.x);
            _weapon.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle * Mathf.Rad2Deg));

            _weapon.transform.position = transform.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
        }
        private void Shoot()
        {
            _weapon.Attack();
        }
    }

}
