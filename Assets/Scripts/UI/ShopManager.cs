using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;


class ShopManager : MonoBehaviour
{
    public PlayerStatManager player;
    public TMP_Text _currencyDisplayText;
    public UI_Manager UIManager;

    private Transform shopItemTemplate;
    private Transform container;

    public List<GameObject> itemsBought;


    private int _sameTypeIndex;
    private int _sameTypeCount = 0;

    public RectTransform[] children;

    private void Awake()
    {
        container = transform.Find("container");
        shopItemTemplate = container.Find("shopItemTemplate");
        shopItemTemplate.gameObject.SetActive(false);
    }

    

    private void Start()
    {
        CreateItem(Item.ItemType.Big_Gun, "Big Gun", "A very big gun. What else do you want?", Item.ItemCost(Item.ItemType.Big_Gun), Item.ItemSprite(Item.ItemType.Big_Gun), 0);
        CreateItem(Item.ItemType.Even_Bigger_gun, "Even bigger gun", "Pew pew, I'm shooting you!", Item.ItemCost(Item.ItemType.Even_Bigger_gun), Item.ItemSprite(Item.ItemType.Even_Bigger_gun), 1);
        CreateItem(Item.ItemType.Big_Gun, "Big Gun", "Hi, I am a placeholder for the descritpion!", Item.ItemCost(Item.ItemType.Katana), Item.ItemSprite(Item.ItemType.Big_Gun), 2);
        CreateItem(Item.ItemType.Big_Gun, "Random name", "Extremely boring random description of an Item", Item.ItemCost(Item.ItemType.Big_Gun), Item.ItemSprite(Item.ItemType.Big_Gun), 3);
        CreateItem(Item.ItemType.Big_Gun, "Big Gun", "A very big gun. What else do you want?", Item.ItemCost(Item.ItemType.Big_Gun), Item.ItemSprite(Item.ItemType.Big_Gun), 4);
        CreateItem(Item.ItemType.Big_Gun, "Big Gun", "A very big gun. What else do you want?", Item.ItemCost(Item.ItemType.Big_Gun), Item.ItemSprite(Item.ItemType.Big_Gun), 5);
        CreateItem(Item.ItemType.Big_Gun, "Big Gun", "A very big gun. What else do you want?", Item.ItemCost(Item.ItemType.Big_Gun), Item.ItemSprite(Item.ItemType.Big_Gun), 6);
        CreateItem(Item.ItemType.Katana, "Katana", "Cool looking Katana", Item.ItemCost(Item.ItemType.Katana), Item.ItemSprite(Item.ItemType.Katana), 7);

        children = gameObject.GetComponentsInChildren<RectTransform>();

    }
    void Update()
    {
    }

    private void CreateItem(Item.ItemType itemType, string itemName, string itemDescription, int itemCost,Sprite sprite, int positionIndex)
    {
        Transform shopItemTransform = Instantiate(shopItemTemplate, container);

        shopItemTransform.gameObject.SetActive(true);
       

        RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();
        float shopItemHeight = 90f;
        shopItemRectTransform.anchoredPosition = new Vector2(1, -shopItemHeight * positionIndex);

        shopItemTransform.Find("itemName").GetComponent<TextMeshProUGUI>().SetText(itemName);
        shopItemTransform.Find("itemDescription").GetComponent<TextMeshProUGUI>().SetText(itemDescription);
        shopItemTransform.Find("itemCost").GetComponent<TextMeshProUGUI>().SetText(itemCost.ToString());
        shopItemTransform.Find("itemIcon").GetComponent<Image>().sprite = Item.ItemSprite(itemType);
        shopItemTransform.Find("itemClass").GetComponent<TextMeshProUGUI>().SetText(Item.AssignClass(itemType).ToString());
        shopItemTransform.Find("damage").GetComponent<TextMeshProUGUI>().SetText(Item.ItemDamage(itemType).ToString());



        shopItemTransform.GetComponent<Button>().onClick.AddListener(delegate { TryBuyItem(itemType,shopItemTransform); });
    }


    public void TryBuyItem(Item.ItemType itemType,Transform shopItemTransform)
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        IShopCustomer shopCustomer = playerObject.GetComponent<IShopCustomer>();
        if (shopCustomer.TrySpendCurrency(Item.ItemCost(itemType)))
        {
            shopCustomer.BoughtItem(itemType);

            // Disabling the interactivity of the button after a purchase
            Button buyButton = shopItemTransform.gameObject.GetComponent<Button>();
            buyButton.interactable = false;
            
            // Graying out the button after an item has been purchased
            var newColorBlock = buyButton.colors;
            newColorBlock.disabledColor = Color.black;
            buyButton.colors = newColorBlock;

            // Enabling the equip button
            Button equipButton =shopItemTransform.Find("equipButton").GetComponent<Button>();
            equipButton.gameObject.SetActive(true);
            equipButton.onClick.AddListener(delegate { EquipCheck(shopItemTransform, itemType,shopCustomer); });
        }
    }

    public void EquipCheck(Transform shopItemTransform, Item.ItemType itemType, IShopCustomer shopCustomer)
    {
        if (player._equippedItems.Count > 0)
        {
            for (int i = 0; i < player._equippedItems.Count; i++)
            {
                if (Item.AssignClass(player._equippedItems[i]) == Item.AssignClass(itemType))
                {
                    _sameTypeIndex = i;
                    _sameTypeCount++;
                }
            }
            if (_sameTypeCount != 0)
            {
                player._equippedItems[_sameTypeIndex] = itemType;
                Equip(shopItemTransform, itemType);
                shopCustomer.EquipItem(itemType);
                Unequip(shopItemTransform, itemType);
            }
            else
            {
                player._equippedItems.Add(itemType);
                shopCustomer.EquipItem(itemType);
                Equip(shopItemTransform, itemType);
            }
        }
        else if (player._equippedItems.Count == 0)
        {
            shopCustomer.EquipItem(itemType);
            Equip(shopItemTransform, itemType);
            player._equippedItems.Add(itemType);

        }
    }
    public void Equip(Transform shopItemTransform, Item.ItemType itemType)
    {
        Transform buttonTransform = shopItemTransform.Find("equipButton");
        Button button = buttonTransform.gameObject.GetComponent<Button>();
        buttonTransform.Find("equip").gameObject.SetActive(false);
        buttonTransform.Find("equipped").gameObject.SetActive(true);
        button.interactable = false;
        _sameTypeCount = 0;

        UIManager.ChangeLoadoutSprite(itemType);
    }

    private void Unequip(Transform shopItemTransform, Item.ItemType itemType)
    {
        for (int i = 0; i < children.Length; i++)
        {
            if (children[i].name == "itemClass")
            {
               if(children[i].GetComponent<TextMeshProUGUI>().text == Item.AssignClass(itemType).ToString() && children[i].gameObject.transform.parent.transform != shopItemTransform)
                {
                    Transform parent = children[i].transform.parent.transform;
                    Transform buttonTransform = parent.Find("equipButton");
                    Transform damageTextTransform = parent.Find("damage");
                    Button button = buttonTransform.gameObject.GetComponent<Button>();
                    string damageText = damageTextTransform.gameObject.GetComponent<TextMeshProUGUI>().text;
                    Transform equipped = buttonTransform.Find("equipped");
                    if(equipped.gameObject.activeSelf == true)
                    {
                        buttonTransform.Find("equip").gameObject.SetActive(true);
                        buttonTransform.Find("equipped").gameObject.SetActive(false);
                        button.interactable = true;
                        player._damage -= int.Parse(damageText);
                    }
                }

            }
         
        }
    }



}
