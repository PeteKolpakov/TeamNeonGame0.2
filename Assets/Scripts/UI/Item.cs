using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType
    {
        Big_Gun,
        Even_Bigger_gun,
        Katana
    }

    public static int ItemCost(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.Big_Gun:          return 50;
            case ItemType.Even_Bigger_gun:  return 120;
            case ItemType.Katana:           return 90;
        }
    }

}
