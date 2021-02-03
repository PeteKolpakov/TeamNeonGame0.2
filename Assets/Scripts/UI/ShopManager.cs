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
    private Transform container;

    public List<Transform> children;

    private int _sameTypeIndex;
    private int _sameTypeCount = 0;

    public List<Object> weaponPrefabList;

    public delegate void AddConsumable(ItemOld.ItemType itemType);
    public static event AddConsumable addConsumable;

    private void Awake()
    {
        container = transform.Find("container");
        shopItemTemplate = container.Find("shopItemTemplate");
        shopItemTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {

        // Grabbing all the weapon prefabs from a folder and putting them in a nice list
        weaponPrefabList = new List<Object>(Resources.LoadAll("WeaponPrefabs", typeof(GameObject)));

        PopulateShopList();
    }
    private void PopulateShopList()
    {
        int positionIndex = 0; // position in the list
        for (int i = 0; i < weaponPrefabList.Count; i++)
        {
            Transform shopItemTransform = Instantiate(shopItemTemplate, container);

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
            shopItemTransform.Find("itemDescription").GetComponent<TextMeshProUGUI>().SetText(itemScript._description);
            shopItemTransform.Find("itemCost").GetComponent<TextMeshProUGUI>().SetText(itemScript._price.ToString());
            shopItemTransform.Find("itemIcon").GetComponent<Image>().sprite = itemScript._icon;

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
    }

    public void BuyItem(Item itemScript, GameObject weapon, Button button, Transform shopItemTransform)
    {
        // Getting a reference for the customer interface
        IShopCustomer shopCustomer = playerTransform.GetComponent<IShopCustomer>();
        if (shopCustomer.TrySpendCurrency(itemScript._price))
        {
            // Buy item
            shopCustomer.BoughtItem(weapon);

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
            equipButton.onClick.AddListener(delegate { EquipCheck(itemScript, weapon, shopCustomer, shopItemTransform); });

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
                player._equippedItems[_sameTypeIndex] = weapon;
                Equip(itemScript, weapon, shopCustomer, shopItemTransform);
                shopCustomer.EquipItem(itemScript);
                Unequip(itemScript, weapon, shopItemTransform);
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
        Transform buttonTransform = shopItemTransform.Find("equipButton");
        Button button = buttonTransform.gameObject.GetComponent<Button>();
        buttonTransform.Find("equip").gameObject.SetActive(false);
        buttonTransform.Find("equipped").gameObject.SetActive(true);
        button.interactable = false;

        UIManager.ChangeLoadoutSprite(weapon, itemScript);
        itemScript._isEquipped = true;

    }

    public void Unequip(Item item, GameObject weapon, Transform shopItemTransform)
    {
        for (int i = 0; i < player._equippedItems.Count; i++)
        {
            if (player._equippedItems[i].GetComponent<Item>().itemType == item.itemType)
            {
                Debug.Log(player._equippedItems[i].name + player._equippedItems[i].GetComponent<Item>()._isEquipped);
            }

        }
    }

    /*private void Unequip(Transform shopItemTransform, ItemOld.ItemType itemType)
    {
        for (int i = 0; i < children.Length; i++)
        {
            if (children[i].name == "itemClass")
            {
               if(children[i].GetComponent<TextMeshProUGUI>().text == ItemOld.AssignClass(itemType).ToString() && children[i].gameObject.transform.parent.transform != shopItemTransform)
                {
                    Transform parent = children[i].transform.parent.transform;
                    Transform buttonTransform = parent.Find("equipButton");
                    //Transform damageTextTransform = parent.Find("damage");
                    Button button = buttonTransform.gameObject.GetComponent<Button>();
                    //string damageText = damageTextTransform.gameObject.GetComponent<TextMeshProUGUI>().text;
                    Transform equipped = buttonTransform.Find("equipped");
                    if(equipped.gameObject.activeSelf == true)
                    {
                        buttonTransform.Find("equip").gameObject.SetActive(true);
                        buttonTransform.Find("equipped").gameObject.SetActive(false);
                        button.interactable = true;
                        //player._damage -= int.Parse(damageText);
                    }
                }

            }
         
        }
    }*/

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
