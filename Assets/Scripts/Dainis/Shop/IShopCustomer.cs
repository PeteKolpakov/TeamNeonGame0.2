using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShopCustomer
{
    void BoughtItem(ReworkedItem item);
    bool TrySpendCurrency(int price);
    void EquipItem(ReworkedItem item);
    void UnequipItem(ReworkedItem item);

}
