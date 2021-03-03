using Assets.Scripts.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class PlayerShoot : AttackBase
{
    Camera _sceneCamera;
    PlayerStatManager _playerStats;
    PlayerMovement playerMovement;
    FallBehaviour fallBehaviour;

    private Vector3 _mousePos;
    public PauseMenuManager pauseMenu;
    //public ShopkeeperInteraction _shop;

    private void Awake()
    {
        _sceneCamera = Camera.main;
        _playerStats = GetComponent<PlayerStatManager>();
        playerMovement = GetComponent<PlayerMovement>();
        fallBehaviour = GetComponent<FallBehaviour>();
    }

    private void Start() {
        CurrentWeapon.projectileAmount =1;
        CurrentWeapon.spreadAngle = 0;
        CurrentWeapon.fireRate = 0.3f;
    }

    protected override void Update()
    {
        base.Update();
        _mousePos = Input.mousePosition;
    }
    protected override void Aim()
    {
        if(pauseMenu._isPauseMenuOpen == false)
        {
            Vector3 mouseWorldSpace = _sceneCamera.ScreenToWorldPoint(_mousePos);

            Vector3 direction = mouseWorldSpace - transform.position;


            float angle = Mathf.Atan2(direction.y, direction.x); // in radians
            _weapon.transform.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg);

            direction.z = 0;
            _weapon.transform.position = transform.position + direction.normalized;
        }
    }

    protected override void Shoot()
    {
        if (fallBehaviour._isFalling == false && Input.GetKey(KeyCode.Mouse0) && pauseMenu._isPauseMenuOpen == false)
        {
            _weapon.Attack();

        }
    }



}