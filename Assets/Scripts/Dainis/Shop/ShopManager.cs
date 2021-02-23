using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Companion;


class ShopManager : MonoBehaviour
{
    public PlayerStatManager player;
    public Transform playerTransform;
    private ShopUIManager UIManager;

    private Transform shopItemTemplate;
    private Transform shopItemTemplateConsumable;
    private Transform shopItemTemplateSkill;

    public Transform weaponContainer;
    public Transform consumableContainer;
    public Transform skillContainer;

    public List<Transform> children;

    private int _sameTypeIndex;
    private int _sameTypeCount = 0;

    public List<Object> weaponPrefabList;
    public List<Object> consumablePrefabList;
    public List<Object> skillPrefabList;

    private void Awake()
    {
        shopItemTemplate = weaponContainer.Find("shopItemTemplate");
        shopItemTemplate.gameObject.SetActive(false);

        shopItemTemplateConsumable = consumableContainer.Find("shopItemTemplate");
        shopItemTemplateConsumable.gameObject.SetActive(false);

        shopItemTemplateSkill = skillContainer.Find("shopItemTemplate");
        shopItemTemplateSkill.gameObject.SetActive(false);

        UIManager = GetComponent<ShopUIManager>();
    }
    private void Start()
    {

        // Grabbing all the weapon prefabs from a folder and putting them in a nice list
        weaponPrefabList = new List<Object>(Resources.LoadAll("GeneratedWeapons", typeof (ReworkedItem)));
        consumablePrefabList = new List<Object>(Resources.LoadAll("ConsumablePrefabs", typeof(ReworkedItem)));
        skillPrefabList = new List<Object>(Resources.LoadAll("SkillPrefabs", typeof(GameObject)));

        PopulateShopList();

        // see the Unequip function to understand what the hell is this thing
        foreach (Transform child in weaponContainer)
        {
            children.Add(child.Find("itemType"));
        }
        foreach (Transform child in consumableContainer)
        {
            children.Add(child.Find("itemType"));
        }

    }
    private void PopulateShopList()
    {
        int positionIndex = 0; // position in the list
        for (int i = 0; i < weaponPrefabList.Count; i++)
        {
            Transform shopItemTransform = Instantiate(shopItemTemplate, weaponContainer);

            // This is a terrible way to do it.
            // But this is the only way to convert List[i] (which is of type Object)
            // to GameObject for further GetComponent method call
            ReworkedItem weaponData = (ReworkedItem)weaponPrefabList[i];

            // Updating the visuals

            ShopItemUI itemUI = shopItemTransform.GetComponent<ShopItemUI>();
            itemUI.SetUIFromItem(weaponData);

            // Positioning an item in the shop list

            shopItemTransform.gameObject.SetActive(true);
            RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();
            float shopItemHeight = 90f;
            shopItemRectTransform.anchoredPosition = new Vector2(1, -shopItemHeight * positionIndex);
            positionIndex++;

            // Adding a button event for "Buy"
            Button button = shopItemTransform.GetComponent<Button>();
            button.onClick.AddListener(delegate { BuyItem(weaponData, button, shopItemTransform); });
        }

        for (int i = 0; i < consumablePrefabList.Count; i++)
        {
            Transform shopItemTransformConsumable = Instantiate(shopItemTemplateConsumable, consumableContainer);

            ReworkedItem consumableData = (ReworkedItem)consumablePrefabList[i];

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
        for (int i = 0; i < skillPrefabList.Count; i++)
        {
            Transform shopItemTransformSkill = Instantiate(shopItemTemplateSkill, skillContainer);

            GameObject skillGO = (GameObject)skillPrefabList[i];
            Skill skillScript =  skillGO.GetComponent<Skill>();

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
        IShopCustomer shopCustomer = playerTransform.GetComponent<IShopCustomer>();
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
        if (player._equippedItems.Count != 0)
        {
            // Iterating through the equipped items to see if an item
            // of the same type is already equipped
            for (int i = 0; i < player._equippedItems.Count; i++)
            {
                // Is this is the case - we store the equipped item's
                // index in the list
                if (player._equippedItems[i].itemType == weaponData.itemType)
                {
                    _sameTypeIndex = i;
                    _sameTypeCount++;
                }
            }

            // Checking for duplicates and replacing them if needed
            if (_sameTypeCount != 0)
            {
                // We replace equipped item of the same type with a new one
                ReworkedItem oldWeapon = player._equippedItems[_sameTypeIndex];
                player._equippedItems[_sameTypeIndex] = weaponData;
                Equip(weaponData, shopCustomer, shopItemTransform);
                shopCustomer.EquipItem(weaponData);
                Unequip(weaponData,shopItemTransform, oldWeapon);
            }
            else
            {
                player._equippedItems.Add(weaponData);
                shopCustomer.EquipItem(weaponData);
                Equip(weaponData, shopCustomer, shopItemTransform);
            }
        }
        // If no items are equipped - equip it right away
        else
        {
            player._equippedItems.Add(weaponData);
            shopCustomer.EquipItem(weaponData);
            Equip(weaponData,shopCustomer, shopItemTransform);
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

        // Update the sprite in the loadout

        UIManager.ChangeLoadoutSprite(weaponData);
    }

    public void Unequip(ReworkedItem weaponData,Transform shopItemTransform, ReworkedItem oldWeapon)
    {
        // THIS SHIT SUCKS SO MUCH I HATE IT
        // thanks god we only run this mess once, when we equip another weapon.

        // basically - we get the "children" list, with all of the
        // itemTypes (melee, ranged) of the weapons in the shop.
       
        // we iterate through this list
        for (int i = 0; i < children.Count; i++)
        {
            // checking if children[i] has the same type as our current weapon
            // (but making sure we're not counting THE current weapon
            if(children[i].GetComponent<TextMeshProUGUI>().text == weaponData.itemType.ToString() && children[i].transform.parent.transform != shopItemTransform)
            {
                // then we find the parent transform, that holds the interactible button
                // and we re-enable it, chaning the text from "equipped" to "equip", 
                // making player able to equip it again
                Transform parent = children[i].transform.parent.transform;
                Transform buttonTransform = parent.Find("equipButton");
                Button button = buttonTransform.gameObject.GetComponent<Button>();
                Transform equipped = buttonTransform.Find("equipped");
                if (equipped.gameObject.activeSelf == true)
                {
                    buttonTransform.Find("equip").gameObject.SetActive(true);
                    buttonTransform.Find("equipped").gameObject.SetActive(false);
                    button.interactable = true;

                    // unequip the old weapon, removing his stats from the player
                    player.UnequipItem(oldWeapon);
                }
            }
        }
        // Is this efficient? Hell no. Does it work? Yes.
    }
}
