using Assets.Scripts.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class PlayerShoot : AttackBase
{
    Camera _sceneCamera;
    PlayerStatManager _playerStats;
<<<<<<< Updated upstream:Assets/Scripts/Dainis/Player/PlayerShoot.cs
    PlayerMovement playerMovement;
=======
>>>>>>> Stashed changes:Assets/Scripts/UI/PlayerShoot.cs

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
<<<<<<< Updated upstream:Assets/Scripts/Dainis/Player/PlayerShoot.cs
        if (Input.GetKeyDown(KeyCode.Mouse0) && _weapon.projectileAmount <= _playerStats._currentAmmoCount && playerMovement.isPauseMenuOpen == false)
        {
            _weapon.Attack();
            _playerStats._currentAmmoCount -= _weapon.projectileAmount;
        }

        // Melee attack 

        if ((Input.GetKeyDown(KeyCode.Mouse1)) && (_meleeWeapon.TryGetComponent(out MeleeWeapon melee)) == true && playerMovement.isPauseMenuOpen == false)
        {

            Debug.Log("Cutting");

            melee.MeleeAttack();

        }
        /*   if ((Input.GetKeyUp(KeyCode.Mouse1)) && (_meleeWeapon.TryGetComponent(out MeleeWeapon melee2)) == true)

               melee2.HideMelee();

       }*/
=======
        if (Input.GetKeyDown(KeyCode.Mouse0))
            _weapon.Attack();
>>>>>>> Stashed changes:Assets/Scripts/UI/PlayerShoot.cs
    }



}