using UnityEngine;

namespace Assets.Scripts.Enemies
{
    class EnemyGun : MonoBehaviour
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
