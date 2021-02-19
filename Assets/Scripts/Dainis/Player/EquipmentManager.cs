using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public PlayerStatManager player;
    public Transform playerTransform;

    public void SetCurrentRangedWeapon(Item weapon, GameObject weaponGO)
    {
        AttackBase attackBase = player.GetComponent<AttackBase>();

        if(weapon.itemType == Item.ItemType.Ranged)
        {
            GameObject oldWeapon = attackBase._weapon.transform.gameObject;
            GameObject newWeapon = Instantiate(weaponGO, playerTransform);
            weaponGO.transform.position = attackBase._weapon.transform.position;
            weaponGO.transform.rotation = attackBase._weapon.transform.rotation;

            attackBase._weapon = newWeapon.GetComponent<Item>();
            Destroy(oldWeapon);

        }

    }
}
