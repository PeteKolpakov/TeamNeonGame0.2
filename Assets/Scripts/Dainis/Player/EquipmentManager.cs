using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    PlayerStatManager player;
    Transform playerTransform;
    public GameObject RangedWeaponTemplatePrefab;
    public GameObject MeleeWeaponTemplatePrefab;

    private void Awake()
    {
        player = GetComponent<PlayerStatManager>();
        playerTransform = transform;
    }

    public void SetCurrentWeapon(ReworkedItem weapon)
    {
        AttackBase attackBase = player.GetComponent<AttackBase>();

        if(weapon.itemType == ItemType.Ranged)
        {
            if(RangedWeaponTemplatePrefab == null)
            {
                Instantiate(RangedWeaponTemplatePrefab, playerTransform);
            }

            Item templateWeaponItem = RangedWeaponTemplatePrefab.GetComponent<Item>();
            templateWeaponItem.AssignData(weapon);


        }
        if(weapon.itemType == ItemType.Melee)
        {
            if (MeleeWeaponTemplatePrefab == null)
            {
                Instantiate(MeleeWeaponTemplatePrefab, playerTransform);
            }

            Item meleetemplateWeaponItem = MeleeWeaponTemplatePrefab.GetComponent<Item>();
            meleetemplateWeaponItem.AssignData(weapon);
        }

    }
}
