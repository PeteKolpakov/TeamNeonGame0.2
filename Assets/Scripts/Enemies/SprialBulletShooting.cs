using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SprialBulletShooting : MonoBehaviour
{
    [NonSerialized]
    public float angle = 0f;
    public GameObject bulletPrefab;
    public GameObject firePoint;
    public float _bulletSpeed = 5f;

    
    void Start()
    {
        //InvokeRepeating("Fire", 0f, 0.3f);
    }

    private void Fire()
    {
        float bulletDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
        float bulletDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

        Vector3 bulletMoveVector = new Vector3(bulletDirX, bulletDirY, 0f);
        Vector2 bulletDirection = (bulletMoveVector - transform.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);

        Rigidbody2D _Bullet = bullet.GetComponent<Rigidbody2D>();

        _Bullet.AddForce(bulletDirection * _bulletSpeed, ForceMode2D.Impulse);

        angle += 10f;
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void OnEnable()
    {
        InvokeRepeating("Fire", 0f, 0.4f);
    }
}
