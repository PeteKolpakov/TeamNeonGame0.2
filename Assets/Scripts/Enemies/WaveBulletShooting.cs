using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveBulletShooting : MonoBehaviour
{
    [SerializeField]
    private int _bulletAmount = 10;

    [SerializeField]
    private float _startAngle = 90f, _endAngle = 270f;

    public GameObject firePoint;
    public GameObject bulletPrefab;
    public float _bulletSpeed = 5f;

    private void Fire()
    {
        float angleStep = (_endAngle - _startAngle) / _bulletAmount;
        float angle = _startAngle;

        for (int i = 0; i < _bulletAmount; i++)
        {
            float bulletDirX = firePoint.transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float bulletDirY = firePoint.transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

            Vector3 bulletMoveVector = new Vector3(bulletDirX, bulletDirY, 0f);
            Vector2 bulletDirection = (bulletMoveVector - firePoint.transform.position).normalized;

            GameObject bullet = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);

            Rigidbody2D _Bullet = bullet.GetComponent<Rigidbody2D>();

            _Bullet.AddForce(bulletDirection * _bulletSpeed, ForceMode2D.Impulse);

            angle += angleStep;

        }

    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void OnEnable()
    {
        InvokeRepeating("Fire", 0f, 2f);
    }
}
