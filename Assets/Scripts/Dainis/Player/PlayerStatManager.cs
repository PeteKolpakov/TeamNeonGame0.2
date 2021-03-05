using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpriteGlow;
using Assets.Scripts.GameManager;


public class PlayerStatManager : MonoBehaviour, IShopCustomer
{
    GlobalUIManager UIManager;
    EquipmentManager EQManager;
    PlayerShoot _playerShoot;

    public int Damage;

    public int Score;

    public List<ReworkedItem> PurchasedItems;
    public List<ReworkedItem> EquippedItems;

    public List<int> BoughtGunsInt;

    SpriteGlowEffect _glow;
    private Color _oldColor;
    private float _oldBrightness;
    private float _oldFireRate;

    private void Start()
    {
        EQManager = GetComponent<EquipmentManager>();
        _playerShoot = GetComponent<PlayerShoot>();

        UIManager = GameObject.FindGameObjectWithTag("GlobalUI").GetComponent<GlobalUIManager>();

        Pickupable.pickupScore += AddScore;
        Pickupable.pickupFR += AddFireRate;

        _glow = GetComponent<SpriteGlowEffect>();
        _oldColor = _glow.GlowColor;
        _oldBrightness = _glow.GlowBrightness;

        _oldFireRate = 0.3f;

        Damage = _playerShoot.CurrentWeapon.damage;
    }

    // This just works beautifully
    public void LoadWeapons(List<int> weaponsBought)
    {
        BoughtGunsInt = weaponsBought;

        List<UnityEngine.Object>CurrentItemList = new List<UnityEngine.Object>(Resources.LoadAll("GeneratedWeapons", typeof(ReworkedItem)));
        for(int i = 0; i < weaponsBought.Count; i++)
        {
            int current = weaponsBought[i];

            for (int x = 0; x < CurrentItemList.Count; x++)
            {
                ReworkedItem currentItem = (ReworkedItem)CurrentItemList[x];
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
        StatsTracker stats = GameObject.FindGameObjectWithTag("GameManager").GetComponent<StatsTracker>();
        stats.Score += plusScore;
    }
    public void RemoveScore(int minusScore){
        Score -= minusScore;
        StatsTracker stats = GameObject.FindGameObjectWithTag("GameManager").GetComponent<StatsTracker>();
        stats.Score -= minusScore;
    }

    public void AddFireRate(float reduction)
    {
        float currentFireRate = _playerShoot.CurrentWeapon.fireRate;
        float newFireRate = currentFireRate - reduction;
        if(newFireRate <= 0)
        {
            newFireRate = 0.1f;
        }
        _playerShoot.CurrentWeapon.fireRate = newFireRate;
        
        // change the weapon shooting mode
        _playerShoot.CurrentWeapon.projectileAmount = 3;
        _playerShoot.CurrentWeapon.spreadAngle = 45;

        BuffVisualActive(_glow);
        StartCoroutine(BuffTimerForFireRate(_oldFireRate, _oldColor, _oldBrightness, _glow));
    }

    private void BuffVisualActive(SpriteGlowEffect glow)
    {
        glow.GlowColor = new Color(0,1,0,255);
        glow.GlowBrightness = 0.868f;
    }

    private void RemoveBuffVisual(Color oldColor, float oldBrightness, SpriteGlowEffect glow)
    {
        glow.GlowColor = oldColor;
        glow.GlowBrightness = oldBrightness;
    }

    public IEnumerator BuffTimerForFireRate(float oldFR, Color oldColor, float oldBrightness, SpriteGlowEffect glow){
        if(_playerShoot != null)
        {
            yield return new WaitForSeconds(3);
            SetFireRateBackToNormal(oldFR);
            RemoveBuffVisual(oldColor, oldBrightness, glow);
        }
    }

    public void SetFireRateBackToNormal(float oldFR){
        // Set the weapon shooting mode back to normal
        _playerShoot.CurrentWeapon.projectileAmount = 1;
        _playerShoot.CurrentWeapon.spreadAngle = 0;
        _playerShoot.CurrentWeapon.fireRate = oldFR;
        StopAllCoroutines();
    }

    private void OnDestroy() 
    {
        StopAllCoroutines();
    }
}

