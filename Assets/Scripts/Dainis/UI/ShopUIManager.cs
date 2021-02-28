using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;
using Assets.Scripts.EntityClass;
using Assets.Scripts.GameManager;
using Assets.Scripts.Player;

class ShopUIManager : MonoBehaviour
{
    [NonSerialized]
    public PlayerStatManager _playerStatManager;
    private PlayerShoot _playerShoot;

    private ShopManager shopManager;
    public TMP_Text MoneyShopDisplay;
    public TMP_Text DamageDisplay;
    public TMP_Text FireRateDisplay;

    public Image firstSlot;
    public Image secondSlot;
    public Image thirdSlot;

    private Image thirdGlobalSlot;

    public GameObject ShopUI;
    private GameObject GlobalUI;
    private bool isShopOpen = false;

    private void Awake()
    {
        GlobalUI = GameObject.FindGameObjectWithTag("GlobalUI");

        shopManager = GetComponent<ShopManager>();
        GlobalUIManager globalUIManager = GlobalUI.GetComponent<GlobalUIManager>();

        thirdGlobalSlot = globalUIManager.thirdGlobalSlot;
    }

    private void Start()
    {
        _playerStatManager = PlayerTracker.Instance.Player.transform.GetComponent<PlayerStatManager>();
        _playerShoot = PlayerTracker.Instance.Player.transform.GetComponent<PlayerShoot>();
        Debug.Log(_playerStatManager._equippedItems);

    }
    private void Update()
    {
        MoneyShopDisplay.text = _playerStatManager._moneyAmount.ToString();
        DamageDisplay.text = _playerStatManager._damage.ToString();
        FireRateDisplay.text = _playerShoot.CurrentWeapon.fireRate.ToString();

        // DEBUG ONLY //
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

        firstSlot.sprite = _playerShoot.CurrentWeapon.icon;
        firstSlot.color = Color.white;
        secondSlot.sprite = _playerShoot.CurrentMeleeWeapon.AccessItemData().icon;
        secondSlot.color = Color.white;

    }
}
