using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ShopManager : MonoBehaviour
{

    public PlayerStatManager player;

    public TMP_Text _currencyDisplayText;

    private Transform shopItemTemplate;
    private Transform container;

    public List<GameObject> itemsBought;

    public ItemSprites itemSprites;

    private void Awake()
    {
        container = transform.Find("container");
        Debug.Log(container);
        shopItemTemplate = container.Find("shopItemTemplate");
        shopItemTemplate.gameObject.SetActive(false);

    }

    private void Start()
    {
        CreateItem(Item.ItemType.Big_Gun, "Big Gun", "A very big gun. What else do you want?", Item.ItemCost(Item.ItemType.Big_Gun),itemSprites.BigGun, 0);
        CreateItem(Item.ItemType.Even_Bigger_gun, "Even bigger gun", "Pew pew, I'm shooting you!", Item.ItemCost(Item.ItemType.Even_Bigger_gun), itemSprites.BiggerGun, 1);
        CreateItem(Item.ItemType.Big_Gun, "Big Gun", "Hi, I am a placeholder for the descritpion!", Item.ItemCost(Item.ItemType.Katana), itemSprites.BiggerGun, 2);
        CreateItem(Item.ItemType.Big_Gun, "Random name", "Extremely boring random description of an Item", Item.ItemCost(Item.ItemType.Big_Gun), itemSprites.Katana, 3);
        CreateItem(Item.ItemType.Big_Gun, "Big Gun", "A very big gun. What else do you want?", Item.ItemCost(Item.ItemType.Big_Gun), itemSprites.Katana, 4);
        CreateItem(Item.ItemType.Big_Gun, "Big Gun", "A very big gun. What else do you want?", Item.ItemCost(Item.ItemType.Big_Gun), itemSprites.BigGun, 5);
        CreateItem(Item.ItemType.Big_Gun, "Big Gun", "A very big gun. What else do you want?", Item.ItemCost(Item.ItemType.Big_Gun), itemSprites.BigGun, 6);

    }
    void Update()
    {
        _currencyDisplayText.text = player._moneyAmount.ToString();
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
        shopItemTransform.Find("itemIcon").GetComponent<Image>().sprite = sprite;


        shopItemTransform.GetComponent<Button>().onClick.AddListener(delegate { TryBuyItem(itemType); });
    }


    public void TryBuyItem(Item.ItemType itemType)
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        IShopCustomer shopCustomer = playerObject.GetComponent<IShopCustomer>();
        if (shopCustomer.TrySpendCurrency(Item.ItemCost(itemType)))
        {
            Debug.Log(itemType + " purchased.");
        }
    }

}
