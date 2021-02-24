using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerStatManager : MonoBehaviour, IShopCustomer
{
    [SerializeField]
    GlobalUIManager UIManager;
    EquipmentManager EQManager;

    public int _maxxArmorPoints = 1;
    public int _currentArmorPoints;
    public float _armorPointHealth = 1; // How much HP is 1 AP

    public int _maxxAmmoCount = 5;
    public int _currentAmmoCount;

    public float _fireRate = 1f;

    public int _damage = 1;

    public int _moneyAmount;

    public List<ReworkedItem> _purchasedItems;
    public List<ReworkedItem> _equippedItems;

    public List<int> BoughtGunsInt;

    private void Awake()
    {
        EQManager = GetComponent<EquipmentManager>();
    }
    private void Start()
    {
        _currentArmorPoints = _maxxArmorPoints;
        _currentAmmoCount = _maxxAmmoCount;

        Pickupable.pickupAmmo += AddAmmo;
        Pickupable.pickupCurrency += AddCurrency;

    }

    private void Update()
    {
        if (_currentAmmoCount > _maxxAmmoCount)
        {
            _currentAmmoCount = _maxxAmmoCount;
        }

    }

    // This just works beautifully
    public void LoadWeapons(List<int> weaponsBought)
    {
        BoughtGunsInt = weaponsBought;

        Debug.Log("StartedLoading");
        Debug.Log(weaponsBought.Count);
        List<UnityEngine.Object>whatever = new List<UnityEngine.Object>(Resources.LoadAll("GeneratedWeapons", typeof(ReworkedItem)));
        for(int i = 0; i < weaponsBought.Count; i++)
        {
            int current = weaponsBought[i];

            for (int x = 0; x < whatever.Count; x++)
            {
                
                ReworkedItem currentItem = (ReworkedItem)whatever[x];
               // Debug.Log(whatever.Count);
                Debug.Log(currentItem.WeaponID);
                if (currentItem.WeaponID == current)
                {
                    _purchasedItems.Add(currentItem);
                    break;
                }

            }
          
        }
       
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

    public void BoughtItem(ReworkedItem item)
    { 
        _purchasedItems.Add(item);
        BoughtGunsInt.Add(item.WeaponID);

      
    }

    public void EquipItem(ReworkedItem item)
    {   
        _damage += item._damage;
        EQManager.SetCurrentWeapon(item);
    }

    public void UnequipItem(ReworkedItem item)
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

