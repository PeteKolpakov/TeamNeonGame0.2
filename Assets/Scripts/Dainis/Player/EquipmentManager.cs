using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public PlayerStatManager player;
    public Transform playerTransform;
    public GameObject weaponTemplatePrefab;

    public void SetCurrentWeapon(ReworkedItem weapon)
    {
        AttackBase attackBase = player.GetComponent<AttackBase>();

        if(weapon.itemType == ItemType.Ranged)
        {
            if(weaponTemplatePrefab == null)
            {
                Instantiate(weaponTemplatePrefab, playerTransform);
            }

            Item templateWeaponItem = weaponTemplatePrefab.GetComponent<Item>();
            templateWeaponItem.AssignData(weapon);


        }
        if(weapon.itemType == ItemType.Melee)
        {
            //attackBase._meleeWeapon = weapon;
        }

    }
}
