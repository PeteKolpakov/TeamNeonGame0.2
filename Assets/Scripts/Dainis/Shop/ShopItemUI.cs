using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShopItemUI : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text itemName;
    [SerializeField] TMPro.TMP_Text itemDescription;
    [SerializeField] TMPro.TMP_Text itemCost;
    [SerializeField] TMPro.TMP_Text itemType;
    [SerializeField] Image itemIcon;
    [SerializeField] Button equipButton;

    public void SetUIFromItem(Item item)
    {
        itemName.SetText(item._name);
        itemDescription.SetText(item._description);
        itemCost.SetText(item._price.ToString());
        itemType.SetText(item.itemType.ToString());
        itemIcon.sprite = item._icon;
    }

    public void SetEquipButton(UnityEngine.Events.UnityAction action)
    {
        equipButton.gameObject.SetActive(true);
        equipButton.onClick.AddListener(action);
    }

}
