using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;


class ShopManager : MonoBehaviour
{
    public PlayerStatManager player;
    public Transform playerTransform;
    public TMP_Text _currencyDisplayText;
    public UI_Manager UIManager;

    private Transform shopItemTemplate;
    private Transform shopItemTemplateConsumable;



    public Transform weaponContainer;
    public Transform consumableContainer;


    public List<Transform> children;

    private int _sameTypeIndex;
    private int _sameTypeCount = 0;

    public List<Object> weaponPrefabList;
    public List<Object> consumablePrefabList;


    public delegate void AddConsumable(ItemOld.ItemType itemType);
    public static event AddConsumable addConsumable;

    // test
    public TextMeshProUGUI nameTextTest;


    private void Awake()
    {
        shopItemTemplate = weaponContainer.Find("shopItemTemplate");
        shopItemTemplate.gameObject.SetActive(false);

        shopItemTemplateConsumable = consumableContainer.Find("shopItemTemplate");
        shopItemTemplateConsumable.gameObject.SetActive(false);

    }

    private void Start()
    {

        // Grabbing all the weapon prefabs from a folder and putting them in a nice list
        weaponPrefabList = new List<Object>(Resources.LoadAll("WeaponPrefabs", typeof(GameObject)));
        consumablePrefabList = new List<Object>(Resources.LoadAll("ConsumablePrefabs", typeof(GameObject)));



        PopulateShopList();

        // see the Unequip function to understand what the hell is this thing
        foreach (Transform child in weaponContainer)
        {
            children.Add( child.Find("itemType"));
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
            GameObject weapon = (GameObject)weaponPrefabList[i];
            // Grabbing a reference for an Item script, that holds all the necessary values
            Item itemScript = weapon.GetComponent<Item>();


            // Updating the visuals

            // I know strings are bad, but we can't reference a specific
            // GameObject in the hierarchy otherwise, since we're always
            // instantiating a new parent with a new set of children.

            shopItemTransform.Find("itemName").GetComponent<TextMeshProUGUI>().SetText(itemScript._name);
            //nameTextTest.SetText(itemScript._name); // doesn't work for the first item in the list :thinking:
            shopItemTransform.Find("itemDescription").GetComponent<TextMeshProUGUI>().SetText(itemScript._description);
            shopItemTransform.Find("itemCost").GetComponent<TextMeshProUGUI>().SetText(itemScript._price.ToString());
            shopItemTransform.Find("itemIcon").GetComponent<Image>().sprite = itemScript._icon;
            shopItemTransform.Find("itemType").GetComponent<TextMeshProUGUI>().SetText(itemScript.itemType.ToString());

            // Positioning an item in the shop list

            shopItemTransform.gameObject.SetActive(true);
            RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();
            float shopItemHeight = 90f;
            shopItemRectTransform.anchoredPosition = new Vector2(1, -shopItemHeight * positionIndex);
            positionIndex++;

            // Adding a button event for "Buy"
            Button button = shopItemTransform.GetComponent<Button>();
            button.onClick.AddListener(delegate { BuyItem(itemScript, weapon, button, shopItemTransform); });

        }

        for (int i = 0; i < consumablePrefabList.Count; i++)
        {
            Transform shopItemTransformConsumable = Instantiate(shopItemTemplateConsumable, consumableContainer);

            GameObject consumable = (GameObject)consumablePrefabList[i];

            Item itemScript = consumable.GetComponent<Item>();


            // Updating the visuals

            // I know strings are bad, but we can't reference a specific
            // GameObject in the hierarchy otherwise, since we're always
            // instantiating a new parent with a new set of children.

            shopItemTransformConsumable.Find("itemName").GetComponent<TextMeshProUGUI>().SetText(itemScript._name);
            //nameTextTest.SetText(itemScript._name); // doesn't work for the first item in the list :thinking:
            shopItemTransformConsumable.Find("itemDescription").GetComponent<TextMeshProUGUI>().SetText(itemScript._description);
            shopItemTransformConsumable.Find("itemCost").GetComponent<TextMeshProUGUI>().SetText(itemScript._price.ToString());
            shopItemTransformConsumable.Find("itemIcon").GetComponent<Image>().sprite = itemScript._icon;
            shopItemTransformConsumable.Find("itemType").GetComponent<TextMeshProUGUI>().SetText(itemScript.itemType.ToString());

            // Positioning an item in the shop list

            shopItemTransformConsumable.gameObject.SetActive(true);
            RectTransform shopItemRectTransform = shopItemTransformConsumable.GetComponent<RectTransform>();
            float shopItemHeight = 90f;
            shopItemRectTransform.anchoredPosition = new Vector2(1, -shopItemHeight * positionIndex);
            positionIndex++;

            // Adding a button event for "Buy"
            Button button = shopItemTransformConsumable.GetComponent<Button>();
            button.onClick.AddListener(delegate { BuyItem(itemScript, consumable, button, shopItemTransformConsumable); });

        }
    }

    public void BuyItem(Item itemScript, GameObject item, Button button, Transform shopItemTransform)
    {
        // Getting a reference for the customer interface
        IShopCustomer shopCustomer = playerTransform.GetComponent<IShopCustomer>();
        if (shopCustomer.TrySpendCurrency(itemScript._price))
        {
            // Buy item
            shopCustomer.BoughtItem(item);

            // Disabling the interactivity of the button after a purchase
            button.interactable = false;

            // Graying out the button after an item has been purchased
            var newColorBlock = button.colors;
            newColorBlock.disabledColor = Color.black;
            button.colors = newColorBlock;

            // Enabling the equip button
            Button equipButton = shopItemTransform.Find("equipButton").GetComponent<Button>();
            equipButton.gameObject.SetActive(true);

            // Adding the "Equip" button event.
            // Since Button.Onclick is always an event -
            // can't do it any other way. But also since the EquipCheck
            // function is in the same class as this function, 
            // we do this magical thing with "delegate" AddListener.
            equipButton.onClick.AddListener(delegate { EquipCheck(itemScript, item, shopCustomer, shopItemTransform); });

        }
    }

    public void EquipCheck(Item itemScript, GameObject weapon, IShopCustomer shopCustomer, Transform shopItemTransform)
    {
        if (player._equippedItems.Count != 0)
        {
            // Iterating through the equipped items to see if an item
            // of the same type is already equipped
            for (int i = 0; i < player._equippedItems.Count; i++)
            {
                Item equippedItemScript = player._equippedItems[i].GetComponent<Item>();

                // Is this is the case - we store the equipped item's
                // index in the list
                if (equippedItemScript.itemType == itemScript.itemType)
                {
                    _sameTypeIndex = i;
                    _sameTypeCount++;
                }
            }

            // Checking for duplicates and replacing them if needed
            if (_sameTypeCount != 0)
            {
                // We replace equipped item of the same type with a new one
                GameObject oldWeapon = player._equippedItems[_sameTypeIndex];
                player._equippedItems[_sameTypeIndex] = weapon;
                Equip(itemScript, weapon, shopCustomer, shopItemTransform);
                shopCustomer.EquipItem(itemScript);
                Unequip(itemScript,shopItemTransform, oldWeapon);
            }
            else
            {
                player._equippedItems.Add(weapon);
                shopCustomer.EquipItem(itemScript);
                Equip(itemScript, weapon, shopCustomer, shopItemTransform);
            }


        }
        // If no items are equipped - equip it right away
        else
        {
            player._equippedItems.Add(weapon);
            shopCustomer.EquipItem(itemScript);
            Equip(itemScript,weapon,shopCustomer, shopItemTransform);
        }
        _sameTypeCount = 0;
    }

    public void Equip(Item itemScript, GameObject weapon, IShopCustomer shopCustomer, Transform shopItemTransform)
    {
        // disabling the "equip" button and changing the display text
        // to "Equipped"
        Transform buttonTransform = shopItemTransform.Find("equipButton");
        Button button = buttonTransform.gameObject.GetComponent<Button>();
        buttonTransform.Find("equip").gameObject.SetActive(false);
        buttonTransform.Find("equipped").gameObject.SetActive(true);
        button.interactable = false;

        // Update the sprite in the loadout

        UIManager.ChangeLoadoutSprite(weapon, itemScript);
        itemScript._isEquipped = true;

    }

    public void Unequip(Item item,Transform shopItemTransform, GameObject oldWeapon)
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
            if(children[i].GetComponent<TextMeshProUGUI>().text == item.itemType.ToString() && children[i].transform.parent.transform != shopItemTransform)
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
                    Item oldWeaponStats = oldWeapon.GetComponent<Item>();
                    player.UnequipItem(oldWeaponStats);
                }
            }
        }

        // Is this efficient? Hell no. Does it work? Yes.
        
    }

    // !!!!!!!!!!!!!!!
    // ignore this for now , this is for later work on consumables 
    // !!!!!!!!!!!!!!

    /*public void RefreshConsumableStock(ItemOld.ItemType itemType)
    {
        string name = itemType.ToString();
        for (int i = 0; i < children.Length; i++)
        {
            if (children[i].name.ToString() == "itemName")
            {
                if (ItemOld.StringSearch(children[i].GetComponent<TextMeshProUGUI>().text).ToString() == name)
                {
                    Transform parent = children[i].parent.transform;

                    parent.GetComponent<Button>().interactable = true;
                    parent.Find("equipButton").Find("equipped").gameObject.SetActive(false);
                    parent.Find("equipButton").Find("equip").gameObject.SetActive(true);
                    parent.Find("equipButton").GetComponent<Button>().interactable = true;
                    parent.Find("equipButton").gameObject.SetActive(false);
                    player._purchasedItems.Remove(itemType);
                    player._equippedItems.Remove(itemType);
                }
                
            }
        }
    }*/

}
