using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpriteGlow;


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
    public List<int> FinishedLevelsInt;

    SpriteGlowEffect glow;
    private Color oldColor;
    private float oldBrightness;
    private float oldFireRate;



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

        glow = GetComponent<SpriteGlowEffect>();
        oldColor = glow.GlowColor;
        oldBrightness = glow.GlowBrightness;

        oldFireRate = playerShoot.CurrentWeapon.fireRate;

    }


    // This just works beautifully for loading the weapons but it is not in use :( RIP -Kiwii
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
        float currentFireRate = playerShoot.CurrentWeapon.fireRate;
        float newFireRate = currentFireRate - reduction;
        if(newFireRate <= 0){
            newFireRate = 0.1f;
        }
        playerShoot.CurrentWeapon.fireRate = newFireRate;
        Debug.Log("FireRate buff acquired");

        BuffVisualActive(glow);
        StartCoroutine(BuffTimerForFireRate(oldFireRate, oldColor, oldBrightness, glow));

    }

    private void BuffVisualActive(SpriteGlowEffect glow){

        glow.GlowColor = new Color(0,1,0,255);
        glow.GlowBrightness = 0.868f;
    }

    private void RemoveBuffVisual(Color oldColor, float oldBrightness, SpriteGlowEffect glow){
        glow.GlowColor = oldColor;
        glow.GlowBrightness = oldBrightness;

    }

    public IEnumerator BuffTimerForFireRate(float oldFR, Color oldColor, float oldBrightness, SpriteGlowEffect glow){
        yield return new WaitForSeconds(4);
        SetFireRateBackToNormal(oldFR);
        RemoveBuffVisual(oldColor, oldBrightness, glow);
    }

    public void SetFireRateBackToNormal(float oldFR){
        Debug.Log("FireRate buff lost lol");
        playerShoot.CurrentWeapon.fireRate = oldFR;

    }

}

