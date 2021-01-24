using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShopCustomer
{
    void BoughtItem(Item.ItemType itemType);
    bool TrySpendCurrency(int price);
    void EquipItem(Item.ItemType itemType);

}
