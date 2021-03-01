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

class GlobalUIManager : MonoBehaviour
{
    private  PlayerStatManager _playerStatManager;
    public HealthBar _playerHealthbar;
    private Entity player;

    public EnemyBase enemyBase;
    public EnemyHealthBar _enemyHealhbar;

    [SerializeField]
    private List<GameObject> _maxAmmoAmount;

    public List<GameObject> _maxArmorPointCount;
    public List<GameObject> _currentArmorPoints;

    public TMP_Text _moneyDisplay;
    public TMP_Text _healthDisplay;

    public Image firstGlobalSlot;
    public Image secondGlobalSlot;
    public Image thirdGlobalSlot;
    private void Awake()
    {
        PlayerBase.removeArmor += RemoveArmor;
    }

    private void Start()
    {
        player = PlayerTracker.Instance.Player.GetComponent<Entity>();
        _playerStatManager = PlayerTracker.Instance.Player.GetComponent<PlayerStatManager>();
        PlayerShoot playerShoot = player.GetComponent<PlayerShoot>();
        playerShoot.CurrentWeapon.removeAmmo += OnRemoveAmmo;


        _playerHealthbar.SetMaxHealth(player.health);

        if(_enemyHealhbar != null)
        {
         _enemyHealhbar.SetMaxHealth(enemyBase.maxHealth);
        }

        SetAmmoCountDisplay();
        SetArmorPointDisplay();
    }

    private void Update()
    {
        PlayerShoot playerShootScript = player.GetComponent<PlayerShoot>();

        _playerHealthbar.SetHealth(player.health);
        _enemyHealhbar.SetHealth(enemyBase.health);

        _moneyDisplay.text = "$: " + _playerStatManager._moneyAmount.ToString();
        _healthDisplay.text = player.health.ToString() + " \\ " + player.maxHealth.ToString();

        // DEBUG ONLY //
        if (Input.GetKeyDown(KeyCode.R))
        {
            _playerStatManager.AddAmmo(3);
            UpdateAmmoUI();
        }
        // DEBUG ONLY //


        // Updating the current weapon loadout visuals

        firstGlobalSlot.sprite = playerShootScript.CurrentWeapon.icon;
        firstGlobalSlot.color = Color.white;
        secondGlobalSlot.sprite = playerShootScript.CurrentMeleeWeapon.AccessItemData().icon;
        secondGlobalSlot.color = Color.white;
    }

    public void SetAmmoCountDisplay()
    {
        for (int i = 0; i < _maxAmmoAmount.Count; i++)
        {
            _maxAmmoAmount[i].SetActive(i < _playerStatManager._maxxAmmoCount);
        }
    }
    private void OnRemoveAmmo()
    {
        UpdateAmmoUI();
    }

    public void UpdateAmmoUI()
    {
        for (int i = 0; i < _maxAmmoAmount.Count; i++)
        {
            _maxAmmoAmount[i].SetActive(i < _playerStatManager._currentAmmoCount);
        }
    }

    public void SetArmorPointDisplay()
    {
        for (int i = 0; i < _playerStatManager._maxxArmorPoints; i++)
        {
            _currentArmorPoints.Add(_maxArmorPointCount[i]);
            _currentArmorPoints[i].SetActive(true);
        }
    }

    public void RemoveArmor()
    {
        for (int i = 0; i < _currentArmorPoints.Count; i++)
        {
            _currentArmorPoints[i].SetActive(false);
            _playerStatManager._currentArmorPoints--;
        }
    }
}
