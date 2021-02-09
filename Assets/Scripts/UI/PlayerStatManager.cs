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


    public delegate void RemoveArmorPoints();
    public static event RemoveArmorPoints removeArmor;
    

    private void Start()
    {
        _currentHealth = _maxHealth;
        _currentArmorPoints = _maxxArmorPoints;
        _currentAmmoCount = _maxxAmmoCount;
    }
    private void Update()
    {                               // should use Die() method in Entity Class
        //if (_currentHealth < 0)
        //{
        //    _currentHealth = 0;
        //    gameObject.SetActive(false);
        //    Debug.Log("You ded, lol");
        //}
    }
    public void HurtPlayer(int damage)
    {
        float _APBlock = _currentArmorPoints * _armorPointHealth;
        float damageTaken = damage - _APBlock;
        if (damageTaken < 0)
        {
            damageTaken = 0;
        }
        _currentHealth -= damageTaken;

        if (damage > _APBlock)
        {
            if (removeArmor != null)
            {
                removeArmor();
            }
        }
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

