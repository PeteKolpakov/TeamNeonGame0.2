using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Player;
using Assets.Scripts.GameManager;
using UnityEngine.UI;
using TMPro;

public class CompareEquipment : MonoBehaviour
{
    public TMP_Text comparativeText;

    public Image PositiveIndicator;
    public Image NegativeIndicator;
    public Image NeutralIndicator;

    private ReworkedItem shopItemData;
    private PlayerShoot player;

    public void AssignShopItemData(ReworkedItem itemData)
    {
        shopItemData = itemData;
    }

    private void Start()
    {
        player = PlayerTracker.Instance.Player.GetComponent<PlayerShoot>();
       
    }

    private void Update()
    {
        CompareRangedWeapons();
        CompareMeleeWeapons();
        //Debug.Log(player.CurrentWeapon.itemName);
    }

    private void CompareRangedWeapons()
    {
        if(shopItemData.itemType == ItemType.Ranged)
        {
            if(shopItemData._damage > player.CurrentWeapon.damage)
            {
                int differenceInDamage = shopItemData._damage - player.CurrentWeapon.damage;
                comparativeText.SetText("+ " + differenceInDamage);
                PositiveIndicator.gameObject.SetActive(true);
                NegativeIndicator.gameObject.SetActive(false);
                NeutralIndicator.gameObject.SetActive(false);
            }
            else if(shopItemData._damage < player.CurrentWeapon.damage)
            {
                int differenceInDamage = player.CurrentWeapon.damage - shopItemData._damage;
                comparativeText.SetText("- " + differenceInDamage);
                PositiveIndicator.gameObject.SetActive(false);
                NegativeIndicator.gameObject.SetActive(true);
                NeutralIndicator.gameObject.SetActive(false);
            }
            else if(shopItemData._damage == player.CurrentWeapon.damage)
            {
                comparativeText.SetText("+ 0");
                PositiveIndicator.gameObject.SetActive(false);
                NegativeIndicator.gameObject.SetActive(false);
                NeutralIndicator.gameObject.SetActive(true);
            }
        }
    }

    private void CompareMeleeWeapons()
    {
        if (shopItemData.itemType == ItemType.Melee)
        {
            if (shopItemData._damage > player.CurrentMeleeWeapon.MeleeDamage)
            {
                int differenceInDamage = shopItemData._damage - player.CurrentMeleeWeapon.MeleeDamage;
                comparativeText.SetText("+ " + differenceInDamage);
                PositiveIndicator.gameObject.SetActive(true);
                NegativeIndicator.gameObject.SetActive(false);
                NeutralIndicator.gameObject.SetActive(false);
            }
            else if (shopItemData._damage < player.CurrentMeleeWeapon.MeleeDamage)
            {
                int differenceInDamage = player.CurrentMeleeWeapon.MeleeDamage - shopItemData._damage;
                comparativeText.SetText("- " + differenceInDamage);
                PositiveIndicator.gameObject.SetActive(false);
                NegativeIndicator.gameObject.SetActive(true);
                NeutralIndicator.gameObject.SetActive(false);
            }
            else if (shopItemData._damage == player.CurrentMeleeWeapon.MeleeDamage)
            {
                comparativeText.SetText("+ 0");
                PositiveIndicator.gameObject.SetActive(false);
                NegativeIndicator.gameObject.SetActive(false);
                NeutralIndicator.gameObject.SetActive(true);
            }
        }
    }
}
