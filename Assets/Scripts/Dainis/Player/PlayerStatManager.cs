using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerStatManager : MonoBehaviour, IShopCustomer
{
    GlobalUIManager UIManager;
    EquipmentManager EQManager;
    PlayerShoot playerShoot;

    public int _damage = 1;

    public int _moneyAmount;

    public List<ReworkedItem> _purchasedItems;
    public List<ReworkedItem> _equippedItems;

    public List<int> BoughtGunsInt;


    private void Awake()
    {
        EQManager = GetComponent<EquipmentManager>();
        playerShoot = GetComponent<PlayerShoot>();
        UIManager = GameObject.FindGameObjectWithTag("GlobalUI").GetComponent<GlobalUIManager>();
    }
    private void Start()
    {
        Pickupable.pickupCurrency += AddCurrency;
        Pickupable.pickupFR += AddFireRate;

    }


    // This just works beautifully
    public void LoadWeapons(List<int> weaponsBought)
    {
        BoughtGunsInt = weaponsBought;

        Debug.Log(weaponsBought.Count);
        List<UnityEngine.Object>CurrentItemList = new List<UnityEngine.Object>(Resources.LoadAll("GeneratedWeapons", typeof(ReworkedItem)));
        for(int i = 0; i < weaponsBought.Count; i++)
        {
            int current = weaponsBought[i];

            for (int x = 0; x < CurrentItemList.Count; x++)
            {
                ReworkedItem currentItem = (ReworkedItem)CurrentItemList[x];
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

    public void AddFireRate(float reduction){
        float oldFireRate = playerShoot.CurrentWeapon.fireRate;
        float newFireRate = oldFireRate - reduction;
        if(newFireRate <= 0){
            newFireRate = 0.1f;
        }
        playerShoot.CurrentWeapon.fireRate = newFireRate;
        Debug.Log("FireRate buff acquired");
        StartCoroutine(BuffTimerForFireRate(oldFireRate));

    }

    public IEnumerator BuffTimerForFireRate(float oldFR){
        yield return new WaitForSeconds(4);
        SetFireRateBackToNormal(oldFR);
    }

    public void SetFireRateBackToNormal(float oldFR){
        Debug.Log("FireRate buff lost lol");
        playerShoot.CurrentWeapon.fireRate = oldFR;

    }

}

