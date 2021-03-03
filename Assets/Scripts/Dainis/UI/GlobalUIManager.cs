using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;
using Assets.Scripts.EntityClass;
using Assets.Scripts.GameManager;
using Assets.Scripts.Player;

class GlobalUIManager : MonoBehaviour
{
    private  PlayerStatManager _playerStatManager;
    public HealthBar _playerHealthbar;

    [SerializeField]
    private Entity player;

    public EnemyBase enemyBase;
    public EnemyHealthBar _enemyHealhbar;

    public TMP_Text ScoreDisplay;
    public TMP_Text _healthDisplay;



    //Debug//
    public TMP_Text firerate;

    

    private void Start()
    {
        player = PlayerTracker.Instance.Player.GetComponent<Entity>();
        _playerStatManager = PlayerTracker.Instance.Player.GetComponent<PlayerStatManager>();
        PlayerShoot playerShoot = player.GetComponent<PlayerShoot>();


        _playerHealthbar.SetMaxHealth(player.health);

        if(_enemyHealhbar != null)
        {
         _enemyHealhbar.SetMaxHealth(enemyBase.maxHealth);
        }
    }

    private void Update()
    {
        PlayerShoot playerShootScript = player.GetComponent<PlayerShoot>();

        _playerHealthbar.SetHealth(player.health);
        _enemyHealhbar.SetHealth(enemyBase.health);

        ScoreDisplay.text =  _playerStatManager._moneyAmount.ToString();
        _healthDisplay.text = player.health.ToString() + " \\ " + player.maxHealth.ToString();


        if(firerate != null){
            firerate.text = "FireRate: " + Math.Round(playerShootScript.CurrentWeapon.fireRate, 1).ToString();

        }
    }
}

