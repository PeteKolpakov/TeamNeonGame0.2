using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 class PlayerShoot : AttackBase
{
    [SerializeField]
    private Camera _sceneCamera;
    [SerializeField]
    private GameObject _bulletPrefab;
    [SerializeField]
    private GameObject _firePoint;

    private Vector3 _mousePos;
    public ShopkeeperInteraction _shop;


    void Start()
    {
        
    }
    protected override void Update()
    {
        base.Update();
        _mousePos = Input.mousePosition; 
    }
    protected override void Aim()
    {
        Vector3 mouseWorldSpace = _sceneCamera.ScreenToWorldPoint(_mousePos);

        Vector3 direction = mouseWorldSpace - transform.position;


        float angle = Mathf.Atan2(direction.y, direction.x); // in radians
        _weapon.transform.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg);
        direction.z = 0;
        _weapon.transform.position = transform.position + direction.normalized;
    }

    protected override void Shoot()
    {
        if (Input.GetMouseButtonDown(1) && _shop._isShopOpen == false)
        {
            for (int i = 0; i < 3; i++)
            {            
                switch (i)
                {
                    case 0:
                        Instantiate(_bulletPrefab, _firePoint.transform.position, _weapon.transform.rotation * Quaternion.Euler(0f, 0f, -25f));                      
                        break;
                    case 1:
                        Instantiate(_bulletPrefab, _firePoint.transform.position, _weapon.transform.rotation);
                        break;
                    case 2:
                        Instantiate(_bulletPrefab, _firePoint.transform.position, _weapon.transform.rotation * Quaternion.Euler(0f, 0f, 25f));
                        break;
                }
            }
        }

        if (Input.GetMouseButtonDown(0)&& _shop._isShopOpen == false)
        {
            Instantiate(_bulletPrefab, _firePoint.transform.position, _weapon.transform.rotation);
        }
    }
}
