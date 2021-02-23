using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;
using Assets.Scripts.EntityClass;

class ShopUIManager : MonoBehaviour
{
    public PlayerStatManager _playerStatManager;

    public ShopManager shopManager;
    public TMP_Text _moneyShopDisplay;

    public Image firstSlot;
    public Image secondSlot;
    public Image thirdSlot;

    public Image firstGlobalSlot;
    public Image secondGlobalSlot;
    public Image thirdGlobalSlot;

    public GameObject ShopUI;
    private bool isShopOpen = false;

    private void Update()
    {
        _moneyShopDisplay.text = _playerStatManager._moneyAmount.ToString();


        // DEBUG ONLY//
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isShopOpen == false)
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
        // DEBUG ONLY //
    }
    public void ChangeLoadoutSprite(ReworkedItem itemData)
    {
        if (itemData.itemType == ItemType.Ranged)
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
