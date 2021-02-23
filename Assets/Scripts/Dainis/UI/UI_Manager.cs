﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;
using Assets.Scripts.EntityClass;

class UI_Manager : MonoBehaviour
{
    public PlayerStatManager _playerStatManager;
    public HealthBar _playerHealthbar;
    public Entity player;
    //public ShopManager shopManager;

    //TODO: DONT FORGET TO REENABLE THIS SHIT

    //public EnemyStatManager _enemyStats;
    //public EnemyHealthBar _enemyHealhbar;


    [SerializeField]
    private List<GameObject> _maxAmmoAmount;

    public List<GameObject> _maxArmorPointCount;
    public List<GameObject> _currentArmorPoints;

    public TMP_Text _moneyDisplay;
    public TMP_Text _moneyShopDisplay;

    public TMP_Text _healthDisplay;
    

    // TODO: put this shit in the ShopManager 

    public Image firstSlot;
    public Image secondSlot;
    public Image thirdSlot;

    public Image firstGlobalSlot;
    public Image secondGlobalSlot;
    public Image thirdGlobalSlot;

    //public Transform consumableSlot;

    public GameObject ShopUI;
    private bool isShopOpen = false;


    private void Awake()
    {
        PlayerShoot playerShoot = player.GetComponent<PlayerShoot>();

        playerShoot.CurrentWeapon.removeAmmo += OnRemoveAmmo;
        PlayerBase.removeArmor += RemoveArmor;
    }



    private void Start()
    {
        _playerHealthbar.SetMaxHealth(player.health);
        //_enemyHealhbar.SetMaxHealth(_enemyStats._maxHealth);

        SetAmmoCountDisplay();
        SetArmorPointDisplay();
    }

    private void Update()
    {
        _playerHealthbar.SetHealth(player.health);
        //_enemyHealhbar.SetHealth(_enemyStats._currentHealth);

        _moneyDisplay.text = "$: " + _playerStatManager._moneyAmount.ToString();
        _moneyShopDisplay.text = _playerStatManager._moneyAmount.ToString();

        _healthDisplay.text = player.health.ToString() + " \\ " + player.maxHealth.ToString();

        //Debug ONLY
        // DELETE AFTER DONE
        if (Input.GetKeyDown(KeyCode.R) && _playerStatManager._currentAmmoCount <= _playerStatManager._maxxAmmoCount)
        {
            _playerStatManager.AddAmmo(3);
            UpdateAmmoUI();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(isShopOpen == false)
            {
                Time.timeScale = 0;
                ShopUI.SetActive(true);
                isShopOpen = true;
            }
            else
            {
                Time.timeScale = 1;
                ShopUI.SetActive(false);
                isShopOpen = false;
            }

        }

    }


    public void SetAmmoCountDisplay()
    {
        for (int i = 0; i < _maxAmmoAmount.Count; i++)
        {
            _maxAmmoAmount[i].SetActive(i < _playerStatManager._maxxAmmoCount);
        }
    }
    private void OnRemoveAmmo(int ammo)
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

    public void ChangeLoadoutSprite(ReworkedItem itemData)
    {
        if(itemData.itemType == ItemType.Ranged )
        {
            firstSlot.sprite = itemData._icon;
            firstSlot.color = Color.white;

            firstGlobalSlot.sprite = itemData._icon;
            firstGlobalSlot.color = Color.white;

        }
        if (itemData.itemType == ItemType.Melee)
        {
            secondSlot.sprite = itemData._icon;
            secondSlot.color = Color.white;

            secondGlobalSlot.sprite = itemData._icon;
            secondGlobalSlot.color = Color.white;
        }
        if (itemData.itemType == ItemType.Consumable)
        {
            thirdSlot.sprite = itemData._icon;
            thirdSlot.color = Color.white;

            thirdGlobalSlot.sprite = itemData._icon;
            thirdGlobalSlot.color = Color.white;
        }
    }

}
