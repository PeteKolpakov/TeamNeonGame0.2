using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShopCustomer
{
    void BoughtItem(GameObject item);
    bool TrySpendCurrency(int price);
    void EquipItem(Item item);
    void UnequipItem(Item item);

}
