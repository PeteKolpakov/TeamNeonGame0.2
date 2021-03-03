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

    [SerializeField]
    private Entity _player;
    public TMP_Text ScoreDisplay;
    public TMP_Text HealthDisplay;

    private void Start()
    {
        _player = PlayerTracker.Instance.Player.GetComponent<Entity>();
        _playerStatManager = PlayerTracker.Instance.Player.GetComponent<PlayerStatManager>();
        PlayerShoot playerShoot = _player.GetComponent<PlayerShoot>();

        PlayerHealthbar.SetMaxHealth(_player.health);
    }

    private void Update()
    {
        PlayerShoot playerShootScript = _player.GetComponent<PlayerShoot>();

        PlayerHealthbar.SetHealth(_player.health);

        ScoreDisplay.text =  _playerStatManager.Score.ToString();
        HealthDisplay.text = _player.health.ToString() + " \\ " + _player.maxHealth.ToString();
    }
}

