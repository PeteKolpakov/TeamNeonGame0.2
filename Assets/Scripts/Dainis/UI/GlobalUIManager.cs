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
    public HealthBar PlayerHealthbar;
    private StatsTracker stats;

    [SerializeField]
    private Entity _player;
    public TMP_Text ScoreDisplay;
    public TMP_Text HealthDisplay;



    private void Start()
    {
        _player = PlayerTracker.Instance.Player.GetComponent<Entity>();
        _playerStatManager = PlayerTracker.Instance.Player.GetComponent<PlayerStatManager>();
        PlayerShoot playerShoot = _player.GetComponent<PlayerShoot>();
        stats = GameObject.FindGameObjectWithTag("GameManager").GetComponent<StatsTracker>();

        PlayerHealthbar.SetMaxHealth(_player.health);
        
        // This is a switch, that controls the start of the timer.
        // We start it at the very first time we load the level
        // and change the bool on StatsTracker. Because StatsTracker
        // doesn't get destroyed between the scenes, bool never changes
        // anymore, so the timer doesn't get reset with the new
        // scene load. I don't know how to do it otherwise...
        if(stats.CanStartTheTimer == true){
            stats.StartTheTimer();
            stats.CanStartTheTimer = false;
        }

    }

    private void Update()
    {
        PlayerShoot playerShootScript = _player.GetComponent<PlayerShoot>();

        PlayerHealthbar.SetHealth(_player.health);

        ScoreDisplay.text = stats.Score.ToString();
        HealthDisplay.text = _player.health.ToString() + " \\ " + _player.maxHealth.ToString();
    }
}

