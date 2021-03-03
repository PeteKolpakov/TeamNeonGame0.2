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

    public int Damage;

    public int Score;

    public List<ReworkedItem> PurchasedItems;
    public List<ReworkedItem> EquippedItems;

    public List<int> BoughtGunsInt;

    SpriteGlowEffect glow;
    private Color _oldColor;
    private float _oldBrightness;
    private float _oldFireRate;




    private void Awake()
    {
        EQManager = GetComponent<EquipmentManager>();
        playerShoot = GetComponent<PlayerShoot>();

        UIManager = GameObject.FindGameObjectWithTag("GlobalUI").GetComponent<GlobalUIManager>();
    }
    private void Start()
    {
        Pickupable.pickupScore += AddScore;
        Pickupable.pickupFR += AddFireRate;

        glow = GetComponent<SpriteGlowEffect>();
        _oldColor = glow.GlowColor;
        _oldBrightness = glow.GlowBrightness;

        _oldFireRate = playerShoot.CurrentWeapon.fireRate;

        Damage = playerShoot.CurrentWeapon.damage;

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
                    PurchasedItems.Add(currentItem);
                    break;
                }
            }
        }
    }

    public bool TrySpendCurrency(int price)
    {
        if(Score >= price)
        {
            Score -= price;
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
        PurchasedItems.Add(item);
        BoughtGunsInt.Add(item.WeaponID);

      
    }

    public void EquipItem(ReworkedItem item)
    {
        Damage += item._damage;
        EQManager.SetCurrentWeapon(item);
    }

    public void UnequipItem(ReworkedItem item)
    {
        Damage -= item._damage;

    }

    public void AddScore(int plusScore)
    {
        Score += plusScore;
    }
    public void RemoveScore(int minusScore){
        Score -= minusScore;
    }

    public void AddFireRate(float reduction){
        float currentFireRate = playerShoot.CurrentWeapon.fireRate;
        float newFireRate = currentFireRate - reduction;
        if(newFireRate <= 0){
            newFireRate = 0.1f;
        }
        playerShoot.CurrentWeapon.fireRate = newFireRate;
        
        // change the weapon shooting mode
        playerShoot.CurrentWeapon.projectileAmount = 3;
        playerShoot.CurrentWeapon.spreadAngle = 45;

        BuffVisualActive(glow);
        StartCoroutine(BuffTimerForFireRate(_oldFireRate, _oldColor, _oldBrightness, glow));

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
        // Set the weapon shooting mode back to normal
        playerShoot.CurrentWeapon.projectileAmount = 1;
        playerShoot.CurrentWeapon.spreadAngle = 0;
        playerShoot.CurrentWeapon.fireRate = oldFR;

    }

}

