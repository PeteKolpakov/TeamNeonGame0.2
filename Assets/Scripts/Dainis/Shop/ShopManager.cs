using Assets.Scripts.GameManager;
using Companion;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


class ShopManager : MonoBehaviour
{
    private PlayerStatManager _player;
    private Transform _playerTransform;
    private ShopUIManager _UIManager;
    private CompareEquipment _compare;

    private Transform _shopItemTemplate;
    private Transform _shopItemTemplateConsumable;
    private Transform _shopItemTemplateSkill;

    public Transform WeaponContainer;
    public Transform ConsumableContainer;
    public Transform SkillContainer;

    public List<Transform> Children;

    private int _sameTypeIndex;
    private int _sameTypeCount = 0;

    public List<Object> WeaponPrefabList;
    public List<Object> ConsumablePrefabList;
    public List<Object> SkillPrefabList;

    private void Awake()
    {
        _shopItemTemplate = WeaponContainer.Find("shopItemTemplate");
        _shopItemTemplate.gameObject.SetActive(false);

        _shopItemTemplateConsumable = ConsumableContainer.Find("shopItemTemplate");
        _shopItemTemplateConsumable.gameObject.SetActive(false);

        _shopItemTemplateSkill = SkillContainer.Find("shopItemTemplate");
        _shopItemTemplateSkill.gameObject.SetActive(false);

        _UIManager = GetComponent<ShopUIManager>();
    }
    private void Start()
    {
        _player = PlayerTracker.Instance.Player.GetComponent<PlayerStatManager>();


        // Grabbing all the weapon prefabs from a folder and putting them in a nice list
        WeaponPrefabList = new List<Object>(Resources.LoadAll("GeneratedWeapons", typeof(ReworkedItem)));
        ConsumablePrefabList = new List<Object>(Resources.LoadAll("ConsumablePrefabs", typeof(ReworkedItem)));
        SkillPrefabList = new List<Object>(Resources.LoadAll("SkillPrefabs", typeof(GameObject)));

        _playerTransform = PlayerTracker.Instance.Player.transform;

        PopulateShopList();

        // see the Unequip function to understand what the hell is this thing
        foreach (Transform child in WeaponContainer)
        {
            Children.Add(child.Find("itemType"));
        }
        foreach (Transform child in ConsumableContainer)
        {
            Children.Add(child.Find("itemType"));
        }
    }
    private void PopulateShopList()
    {
        int positionIndex = 0; // position in the list
        for (int i = 0; i < WeaponPrefabList.Count; i++)
        {
            Transform shopItemTransform = Instantiate(_shopItemTemplate, WeaponContainer);

            // This is a terrible way to do it.
            // But this is the only way to convert List[i] (which is of type Object)
            // to GameObject for further GetComponent method call
            ReworkedItem weaponData = (ReworkedItem)WeaponPrefabList[i];

            // Updating the visuals

            ShopItemUI itemUI = shopItemTransform.GetComponent<ShopItemUI>();
            itemUI.SetUIFromItem(weaponData);

            // Positioning an item in the shop list

            shopItemTransform.gameObject.SetActive(true);
            RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();
            float shopItemHeight = 90f;
            shopItemRectTransform.anchoredPosition = new Vector2(1, -shopItemHeight * positionIndex);
            positionIndex++;

            _compare = shopItemTransform.GetComponent<CompareEquipment>();
            _compare.AssignShopItemData(weaponData);

            // Adding a button event for "Buy"
            Button button = shopItemTransform.GetComponent<Button>();
            button.onClick.AddListener(delegate { BuyItem(weaponData, button, shopItemTransform); });
        }

        for (int i = 0; i < ConsumablePrefabList.Count; i++)
        {
            Transform shopItemTransformConsumable = Instantiate(_shopItemTemplateConsumable, ConsumableContainer);

            ReworkedItem consumableData = (ReworkedItem)ConsumablePrefabList[i];

            // Updating the visuals

            ShopItemUI itemUI = shopItemTransformConsumable.GetComponent<ShopItemUI>();
            itemUI.SetUIFromItem(consumableData);

            // Positioning an item in the shop list

            shopItemTransformConsumable.gameObject.SetActive(true);
            RectTransform shopItemRectTransform = shopItemTransformConsumable.GetComponent<RectTransform>();
            float shopItemHeight = 90f;
            shopItemRectTransform.anchoredPosition = new Vector2(1, -shopItemHeight * positionIndex);
            positionIndex++;

            // Adding a button event for "Buy"
            Button button = shopItemTransformConsumable.GetComponent<Button>();
            button.onClick.AddListener(delegate { BuyItem(consumableData, button, shopItemTransformConsumable); });
        }
        for (int i = 0; i < SkillPrefabList.Count; i++)
        {
            Transform shopItemTransformSkill = Instantiate(_shopItemTemplateSkill, SkillContainer);

            GameObject skillGO = (GameObject)SkillPrefabList[i];
            Skill skillScript = skillGO.GetComponent<Skill>();

            // Updating the visuals

            ShopItemUI itemUI = shopItemTransformSkill.GetComponent<ShopItemUI>();
            itemUI.SetUIFromSkillName(skillScript.GetSkillName());
            itemUI.SetUIFromSkillDescription(skillScript.GetDescription());

            // Positioning an item in the shop list

            shopItemTransformSkill.gameObject.SetActive(true);
            RectTransform shopItemRectTransform = shopItemTransformSkill.GetComponent<RectTransform>();
            float shopItemHeight = 90f;
            shopItemRectTransform.anchoredPosition = new Vector2(1, -shopItemHeight * positionIndex);
            positionIndex++;

            // Adding a button event for "Buy"
            Button button = shopItemTransformSkill.GetComponent<Button>();
            //button.onClick.AddListener(delegate { BuyItem(itemScript, consumable, button, shopItemTransformConsumable); });
        }
    }

    public void BuyItem(ReworkedItem weaponData, Button button, Transform shopItemTransform)
    {
        // Getting a reference for the customer interface
        IShopCustomer shopCustomer = _playerTransform.GetComponent<IShopCustomer>();
        if (shopCustomer.TrySpendCurrency(weaponData._price))
        {
            // Buy item
            shopCustomer.BoughtItem(weaponData);

            // Disabling the interactivity of the button after a purchase
            button.interactable = false;

            // Graying out the button after an item has been purchased
            var newColorBlock = button.colors;
            newColorBlock.disabledColor = Color.black;
            button.colors = newColorBlock;

            // Enabling the equip button
            ShopItemUI itemUI = shopItemTransform.GetComponent<ShopItemUI>();

            // Adding the "Equip" button event.
            // Since Button.Onclick is always an event -
            // can't do it any other way. But also since the EquipCheck
            // function is in the same class as this function, 
            // we do this magical thing with "delegate" AddListener.
            itemUI.SetEquipButton(delegate { EquipCheck(weaponData, shopCustomer, shopItemTransform); });
        }
    }

    public void EquipCheck(ReworkedItem weaponData, IShopCustomer shopCustomer, Transform shopItemTransform)
    {
        if (_player.EquippedItems.Count != 0)
        {
            // Iterating through the equipped items to see if an item
            // of the same type is already equipped
            for (int i = 0; i < _player.EquippedItems.Count; i++)
            {
                // Is this is the case - we store the equipped item's
                // index in the list
                if (_player.EquippedItems[i].itemType == weaponData.itemType)
                {
                    _sameTypeIndex = i;
                    _sameTypeCount++;
                }
            }

            // Checking for duplicates and replacing them if needed
            if (_sameTypeCount != 0)
            {
                // We replace equipped item of the same type with a new one
                ReworkedItem oldWeapon = _player.EquippedItems[_sameTypeIndex];
                _player.EquippedItems[_sameTypeIndex] = weaponData;
                Equip(weaponData, shopCustomer, shopItemTransform);
                shopCustomer.EquipItem(weaponData);
                Unequip(weaponData, shopItemTransform, oldWeapon);
            }
            else
            {
                _player.EquippedItems.Add(weaponData);
                shopCustomer.EquipItem(weaponData);
                Equip(weaponData, shopCustomer, shopItemTransform);
            }
        }
        // If no items are equipped - equip it right away
        else
        {
            _player.EquippedItems.Add(weaponData);
            shopCustomer.EquipItem(weaponData);
            Equip(weaponData, shopCustomer, shopItemTransform);
        }
        _sameTypeCount = 0;
    }

    public void Equip(ReworkedItem weaponData, IShopCustomer shopCustomer, Transform shopItemTransform)
    {
        // disabling the "equip" button and changing the display text
        // to "Equipped"
        Transform buttonTransform = shopItemTransform.Find("equipButton");
        Button button = buttonTransform.gameObject.GetComponent<Button>();
        buttonTransform.Find("equip").gameObject.SetActive(false);
        buttonTransform.Find("equipped").gameObject.SetActive(true);
        button.interactable = false;

    }

    public void Unequip(ReworkedItem weaponData, Transform shopItemTransform, ReworkedItem oldWeapon)
    {
        // thanks god we only run this mess once, when we equip another weapon.

        // basically - we get the "children" list, with all of the
        // itemTypes (melee, ranged) of the weapons in the shop.

        // we iterate through this list
        for (int i = 0; i < Children.Count; i++)
        {
            // checking if children[i] has the same type as our current weapon
            // (but making sure we're not counting THE current weapon
            if (Children[i].GetComponent<TextMeshProUGUI>().text == weaponData.itemType.ToString() && Children[i].transform.parent.transform != shopItemTransform)
            {
                // then we find the parent transform, that holds the interactible button
                // and we re-enable it, chaning the text from "equipped" to "equip", 
                // making player able to equip it again
                Transform parent = Children[i].transform.parent.transform;
                Transform buttonTransform = parent.Find("equipButton");
                Button button = buttonTransform.gameObject.GetComponent<Button>();
                Transform equipped = buttonTransform.Find("equipped");
                if (equipped.gameObject.activeSelf == true)
                {
                    buttonTransform.Find("equip").gameObject.SetActive(true);
                    buttonTransform.Find("equipped").gameObject.SetActive(false);
                    button.interactable = true;

                    // unequip the old weapon, removing his stats from the player
                    _player.UnequipItem(oldWeapon);
                }
            }
        }
        // Is this efficient? Hell no. Does it work? Yes.
    }

}
