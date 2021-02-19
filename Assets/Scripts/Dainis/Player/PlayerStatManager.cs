﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerStatManager : MonoBehaviour, IShopCustomer
{
    [SerializeField]
    UI_Manager UIManager;
    [SerializeField]
    EquipmentManager EQManager;

    public int _maxxArmorPoints = 1;
    public int _currentArmorPoints;
    public float _armorPointHealth = 1; // How much HP is 1 AP

    public int _maxxAmmoCount = 5;
    public int _currentAmmoCount;

    public float _fireRate = 1f;

    public int _damage = 1;

    public int _moneyAmount;

    public List<GameObject> _purchasedItems;
    public List<GameObject> _equippedItems;

    public bool _canEquipItem;

    
    private void Start()
    {
        _currentArmorPoints = _maxxArmorPoints;
        _currentAmmoCount = _maxxAmmoCount;

        Pickupable.pickupAmmo += AddAmmo;
        Pickupable.pickupCurrency += AddCurrency;
    }

    public bool TrySpendCurrency(int price)
    {
        if(_moneyAmount >= price)
        {
            _moneyAmount -= price;
            return true;

        }
        else
        {
            Debug.Log("Not enough money!");

            return false;
        }
    }

    public void BoughtItem(GameObject item)
    { 
        _purchasedItems.Add(item);
    }

    public void EquipItem(Item item, GameObject weapon)
    {   
        _damage += item._damage;
        EQManager.SetCurrentRangedWeapon(item, weapon);
    }

    public void UnequipItem(Item item)
    {
        _damage -= item._damage;

    }

    public void AddCurrency(int currency)
    {
        _moneyAmount += currency;
    }

    public void AddAmmo(int ammo)
    {
        _currentAmmoCount += ammo;
        UIManager.UpdateAmmoUI();
    }


}

