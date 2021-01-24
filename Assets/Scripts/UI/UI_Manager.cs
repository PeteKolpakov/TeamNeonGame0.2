using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;

class UI_Manager : MonoBehaviour
{
    public PlayerStatManager _playerStatManager;
    public HealthBar _playerHealthbar;
    public ShopManager shopManager;

    public EnemyStatManager _enemyStats;
    public EnemyHealthBar _enemyHealhbar;


    [SerializeField]
    private List<GameObject> _maxAmmoAmount;
    public List<GameObject> _currentAmmoAmount;
    public List<GameObject> _removedAmmo;

    public List<GameObject> _maxArmorPointCount;
    public List<GameObject> _currentArmorPoints;

    public TMP_Text _moneyDisplay;
    public TMP_Text _healthDisplay;
    public TMP_Text _consumableCharges;
    public TMP_Text _consumableTypeText;
    public TMP_Text _compareEquipmentText;

    public Image firstSlot;
    public Image secondSlot;
    public Image thirdSlot;

    public Transform consumableSlot;

    public int _charges = 2;


    private void Awake()
    {
        PlayerShoot.removeAmmo += RemoveAmmo;
        PlayerStatManager.removeArmor += RemoveArmor;
        ShopManager.addConsumable += AddConsumable;
    }


    private void Start()
    {
        _playerHealthbar.SetMaxHealth(_playerStatManager._maxHealth);
        _enemyHealhbar.SetMaxHealth(_enemyStats._maxHealth);
       
        SetAmmoCountDisplay();
        SetArmorPointDisplay();
    }

    private void Update()
    {
        _playerHealthbar.SetHealth(_playerStatManager._currentHealth);
        _enemyHealhbar.SetHealth(_enemyStats._currentHealth);

        _moneyDisplay.text = "$: " +_playerStatManager._moneyAmount.ToString();
        _healthDisplay.text = _playerStatManager._currentHealth.ToString() + " \\ " + _playerStatManager._maxHealth.ToString();

        //Debug ONLY
        // DELETE AFTER DONE
        if (Input.GetKeyDown(KeyCode.H))
        {
            _playerStatManager.HurtPlayer(2);
        }
        if (Input.GetKeyDown(KeyCode.G)&& _playerStatManager._currentAmmoCount != _playerStatManager._maxxAmmoCount)
        {
            _playerStatManager._currentAmmoCount += 1;
            AddAmmo(1);
        }
        if (Input.GetKeyDown(KeyCode.Q) && _charges != 0)
        {
            UseConsumable();

        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            CompareEquipment();
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

    public void ChangeLoadoutSprite(Item.ItemType itemType)
    {
        Sprite sprite = Item.ItemSprite(itemType);
        if(Item.AssignClass(itemType).ToString() == "Ranged" )
        {
            firstSlot.sprite = sprite;
            firstSlot.color = Color.white;
        }
        if (Item.AssignClass(itemType).ToString() == "Melee")
        {
            secondSlot.sprite = sprite;
            secondSlot.color = Color.white;
        }
        if (Item.AssignClass(itemType).ToString() == "Consumable")
        {
            thirdSlot.sprite = sprite;
            thirdSlot.color = Color.red;
        }

    }

    public void AddConsumable(Item.ItemType itemType)
    {
        //Debug.Log("purchased a potion");
        _charges++;
        _consumableCharges.text = _charges.ToString();
        _consumableCharges.gameObject.SetActive(true);
        _consumableTypeText.text = itemType.ToString();
    }

    public void UseConsumable()
    {
        Item.ItemType itemType = Item.StringSearch(consumableSlot.Find("consumableType").GetComponent<TextMeshProUGUI>().text);
        _charges--;
        _consumableCharges.text = _charges.ToString();


        //effect of the consumable
        // switch statement with different potion options
        // each one of them invokes an event on PlayerStatManager

        if(_charges == 0)
        {
            shopManager.RefreshConsumableStock(itemType);
            _consumableCharges.gameObject.SetActive(false);
            thirdSlot.sprite = null;
            thirdSlot.color = Color.gray;
            _consumableTypeText.text = null;
        }
    }

    public void CompareEquipment()
    {
        Debug.Log("Starting to compare equipment");
        for (int i = 0; i < shopManager.children.Length; i++)
        {
            Debug.Log("Going through the children");

            if (shopManager.children[i].name == "ATKstatText")
            {
                Debug.Log("Found one text");

                Transform parent = shopManager.children[i].parent.parent;
                string name = parent.Find("itemName").name.ToString();
                Item.ItemType itemType = Item.StringSearch(name);
                int itemDamage = Item.ItemDamage(itemType);

                if(itemDamage > _playerStatManager._damage)
                {
                    Debug.Log("Modifying text");

                    _compareEquipmentText.text = "+ " + (itemDamage - _playerStatManager._damage);
                } else if(itemDamage == _playerStatManager._damage)
                {
                    _compareEquipmentText.text = "+ 0";
                } else if (itemDamage < _playerStatManager._damage)
                {
                    _compareEquipmentText.text = "- " + (itemDamage - _playerStatManager._damage);

                }
            }
        }
    }
}
