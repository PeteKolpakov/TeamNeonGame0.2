using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;
using Assets.Scripts.EntityClass;

class GlobalUIManager : MonoBehaviour
{
    public PlayerStatManager _playerStatManager;
    public HealthBar _playerHealthbar;
    public Entity player;

    public EnemyBase enemyBase;
    public EnemyHealthBar _enemyHealhbar;

    [SerializeField]
    private List<GameObject> _maxAmmoAmount;

    public List<GameObject> _maxArmorPointCount;
    public List<GameObject> _currentArmorPoints;

    public TMP_Text _moneyDisplay;
    public TMP_Text _healthDisplay;

    private void Awake()
    {
        PlayerBase.removeArmor += RemoveArmor;

        _playerHealthbar.SetMaxHealth(player.health);
        _enemyHealhbar.SetMaxHealth(enemyBase.maxHealth);

        SetAmmoCountDisplay();
        SetArmorPointDisplay();
    }

    private void Start()
    {
        PlayerShoot playerShoot = player.GetComponent<PlayerShoot>();
        playerShoot.CurrentWeapon.removeAmmo += OnRemoveAmmo;
    }

    private void Update()
    {
        _playerHealthbar.SetHealth(player.health);
        _enemyHealhbar.SetHealth(enemyBase.health);

        _moneyDisplay.text = "$: " + _playerStatManager._moneyAmount.ToString();
        _healthDisplay.text = player.health.ToString() + " \\ " + player.maxHealth.ToString();

        // DEBUG ONLY //
        if (Input.GetKeyDown(KeyCode.R))
        {
            _playerStatManager.AddAmmo(3);
            UpdateAmmoUI();
        }
        // DEBUG ONLY //



    }

    public void SetAmmoCountDisplay()
    {
        for (int i = 0; i < _maxAmmoAmount.Count; i++)
        {
            _maxAmmoAmount[i].SetActive(i < _playerStatManager._maxxAmmoCount);
        }
    }
    private void OnRemoveAmmo()
    {
        UpdateAmmoUI();
    }

    public void UpdateAmmoUI()
    {
        for (int i = 0; i < _maxAmmoAmount.Count; i++)
        {
            _maxAmmoAmount[i].SetActive(i < _playerStatManager._currentAmmoCount);
        }
    }

    public void SetArmorPointDisplay()
    {
        for (int i = 0; i < _playerStatManager._maxxArmorPoints; i++)
        {
            _currentArmorPoints.Add(_maxArmorPointCount[i]);
            _currentArmorPoints[i].SetActive(true);
        }
    }

    public void RemoveArmor()
    {
        for (int i = 0; i < _currentArmorPoints.Count; i++)
        {
            _currentArmorPoints[i].SetActive(false);
            _playerStatManager._currentArmorPoints--;
        }
    }
}
