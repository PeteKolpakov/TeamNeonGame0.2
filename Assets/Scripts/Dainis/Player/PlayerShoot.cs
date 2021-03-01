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

    private Vector3 _mousePos;
    //public ShopkeeperInteraction _shop;

    private void Awake()
    {
        _sceneCamera = Camera.main;
        _playerStats = GetComponent<PlayerStatManager>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    protected override void Update()
    {
        base.Update();
        _mousePos = Input.mousePosition;
    }
    protected override void Aim()
    {
        if(playerMovement.isPauseMenuOpen == false)
        {
            Vector3 mouseWorldSpace = _sceneCamera.ScreenToWorldPoint(_mousePos);

            Vector3 direction = mouseWorldSpace - transform.position;


            float angle = Mathf.Atan2(direction.y, direction.x); // in radians
            _weapon.transform.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg);
            if (_meleeWeapon != null)
            {
                _meleeWeapon.transform.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg);

            }

            direction.z = 0;
            _weapon.transform.position = transform.position + direction.normalized;
            if (_meleeWeapon != null)
            {
                _meleeWeapon.transform.position = transform.position + direction.normalized;

            }

        }


    }

    protected override void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && playerMovement.isPauseMenuOpen == false)
        {
            _weapon.Attack();

        }

        // Melee attack 

        if ((Input.GetKeyDown(KeyCode.Mouse1)) && (_meleeWeapon.TryGetComponent(out MeleeWeapon melee)) == true && playerMovement.isPauseMenuOpen == false)
        {

            Debug.Log("Cutting");

            melee.MeleeAttack();

        }
    }



}