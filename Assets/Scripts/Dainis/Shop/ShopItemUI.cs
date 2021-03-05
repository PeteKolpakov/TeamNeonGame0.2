using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class ShopItemUI : MonoBehaviour
{
    [SerializeField] TMP_Text _itemName;
    [SerializeField] TMP_Text _itemDescription;
    [SerializeField] TMP_Text _itemCost;
    [SerializeField] TMP_Text _itemType;
    [SerializeField] Image _itemIcon;
    [SerializeField] Button _equipButton;

    public void SetUIFromItem(ReworkedItem weaponData)
    {
        _itemName.SetText(weaponData._name);
        _itemDescription.SetText(weaponData._description);
        _itemCost.SetText(weaponData._price.ToString());
        _itemType.SetText(weaponData.itemType.ToString());
        _itemIcon.sprite = weaponData._icon;
    }

    public void SetUIFromSkillName(string text)
    {
        _itemName.SetText(text);
    }
    public void SetUIFromSkillDescription(string text)
    {
        _itemDescription.SetText(text);
    }

    public void SetEquipButton(UnityEngine.Events.UnityAction action)
    {
        _equipButton.gameObject.SetActive(true);
        _equipButton.onClick.AddListener(action);
    }
}
