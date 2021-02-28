using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShopCustomer
{
<<<<<<< Updated upstream:Assets/Scripts/Dainis/Shop/IShopCustomer.cs
    void BoughtItem(ReworkedItem item);
    bool TrySpendCurrency(int price);
    void EquipItem(ReworkedItem item);
    void UnequipItem(ReworkedItem item);
=======
    void BoughtItem(GameObject item);
    bool TrySpendCurrency(int price);
    void EquipItem(Item item);
    void UnequipItem(Item item);
>>>>>>> Stashed changes:Assets/Scripts/UI/IShopCustomer.cs

}
