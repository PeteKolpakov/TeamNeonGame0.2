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

    public enum ItemClass
    {
        Ranged,
        Melee,
        Consumable,
        Upgrade
    }

    public static int ItemCost(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.Big_Gun: return 50;
            case ItemType.Even_Bigger_gun: return 120;
            case ItemType.Katana: return 90;
        }
    }

    public static int ItemDamage(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.Big_Gun: return 1;
            case ItemType.Even_Bigger_gun: return 3;
            case ItemType.Katana: return 2;
        }
    }

    public static ItemClass AssignClass(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.Big_Gun: return ItemClass.Ranged;
            case ItemType.Even_Bigger_gun: return ItemClass.Ranged;
            case ItemType.Katana: return ItemClass.Melee;
        }
    }

    public static Sprite ItemSprite(ItemType itemType)
    {

        switch (itemType)
        {
            default:
            case ItemType.Big_Gun: return ItemSprites.i.circle;
            case ItemType.Even_Bigger_gun: return ItemSprites.i.square;
            case ItemType.Katana: return ItemSprites.i.triangle;
        }
    }

}
