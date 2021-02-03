using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class PlayerShoot : AttackBase
{
    [SerializeField]
    Camera _sceneCamera;

    [SerializeField]
    PlayerStatManager _playerStats;

    private Vector3 _mousePos;
    public ShopkeeperInteraction _shop;

    public delegate void RemoveAmmo(int ammo);
    public static event RemoveAmmo removeAmmo;

    private bool _canShoot = true;
    private float nextFireTime;

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
        if (Input.GetKeyDown(KeyCode.Mouse0))
            _weapon.Attack();
    }
}
        
        
    


