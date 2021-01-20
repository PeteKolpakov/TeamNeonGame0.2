using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


class UI_Manager : MonoBehaviour
{
    public PlayerStatManager _playerStatManager;
    public HealthBar _playerHealthbar;

    [SerializeField]
    private List<GameObject> _maxAmmoAmount;
    public List<GameObject> _currentAmmoAmount;
    public List<GameObject> _removedAmmo;

    public List<GameObject> _maxArmorPointCount;
    public List<GameObject> _currentArmorPoints;


    private void Awake()
    {
        PlayerShoot.removeAmmo += RemoveAmmo;
        PlayerStatManager.removeArmor += RemoveArmor;
    }


    private void Start()
    {
        _playerHealthbar.SetMaxHealth(_playerStatManager._maxHealth);
        SetAmmoCountDisplay();
        SetArmorPointDisplay();
    }

    private void Update()
    {
        _playerHealthbar.SetHealth(_playerStatManager._currentHealth);

        //Debug ONLY
        // DELETE AFTER DONE
        if (Input.GetKeyDown(KeyCode.H))
        {
            _playerStatManager.HurtPlayer(7);
        }
        if (Input.GetKeyDown(KeyCode.G)&& _playerStatManager._currentAmmoCount != _playerStatManager._maxxAmmoCount)
        {
            _playerStatManager._currentAmmoCount += 1;
            AddAmmo(1);
        }
        // DELETE THIS SHIT AFTERWARDS
    }

    public void SetAmmoCountDisplay()
    {
        for (int i = 0; i < _playerStatManager._maxxAmmoCount; i++)
        {
            _currentAmmoAmount.Add(_maxAmmoAmount[i]);
            _currentAmmoAmount[i].SetActive(true);
        }
    }
    public void RemoveAmmo(int count)
    {
        
            for (int i = 1; i < count + 1; i++)
            {
                int pos = _currentAmmoAmount.Count - 1;
                _currentAmmoAmount[pos].SetActive(false);
                _removedAmmo.Add(_currentAmmoAmount[pos]);
                _currentAmmoAmount.RemoveAt(pos);
            }
        

    }
    public void AddAmmo(int count)
    {
        
            for (int i = 1; i < count + 1; i++)
            {
                int pos = _removedAmmo.Count - 1;
                _currentAmmoAmount.Add(_removedAmmo[pos]);
                _removedAmmo.RemoveAt(pos);
                _currentAmmoAmount[_currentAmmoAmount.Count - 1].SetActive(true);
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
