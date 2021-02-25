using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class ShopItemUI : MonoBehaviour
{
    [SerializeField] TMP_Text itemName;
    [SerializeField] TMP_Text itemDescription;
    [SerializeField] TMP_Text itemCost;
    [SerializeField] TMP_Text itemType;
    [SerializeField] Image itemIcon;
    [SerializeField] Button equipButton;

    public void SetUIFromItem(ReworkedItem weaponData)
    {
        itemName.SetText(weaponData._name);
        itemDescription.SetText(weaponData._description);
        itemCost.SetText(weaponData._price.ToString());
        itemType.SetText(weaponData.itemType.ToString());
        itemIcon.sprite = weaponData._icon;
    }

    public void SetUIFromSkillName(string text)
    {
        itemName.SetText(text);
    }
    public void SetUIFromSkillDescription(string text)
    {
        itemDescription.SetText(text);
    }



    public void SetEquipButton(UnityEngine.Events.UnityAction action)
    {
        equipButton.gameObject.SetActive(true);
        equipButton.onClick.AddListener(action);
    }

}
