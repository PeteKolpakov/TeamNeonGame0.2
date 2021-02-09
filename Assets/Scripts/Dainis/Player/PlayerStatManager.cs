using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerStatManager : MonoBehaviour, IShopCustomer
{
    public int _maxHealth = 5;
    public float _currentHealth;

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
        _currentHealth = _maxHealth;
        _currentArmorPoints = _maxxArmorPoints;
        _currentAmmoCount = _maxxAmmoCount;
    }

    public bool TrySpendCurrency(int price)
    {
        if(_moneyAmount >= price)
        {
            _moneyAmount -= price;
            return true;
        } else
        {
            return false;
        }
    }

    public void BoughtItem(GameObject item)
    { 
        _purchasedItems.Add(item);
    }

    public void EquipItem(Item item)
    {   
        _damage += item._damage;
    }

    public void UnequipItem(Item item)
    {
        _damage -= item._damage;

    }


}

