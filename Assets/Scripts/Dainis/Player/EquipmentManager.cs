using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    PlayerStatManager player;
    Transform playerTransform;
    public GameObject rangedWeaponTemplatePrefab;
    public GameObject meleeWeaponTemplatePrefab;

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
            if(rangedWeaponTemplatePrefab == null)
            {
                Instantiate(rangedWeaponTemplatePrefab, playerTransform);
            }

            Item templateWeaponItem = rangedWeaponTemplatePrefab.GetComponent<Item>();
            templateWeaponItem.AssignData(weapon);


        }
        if(weapon.itemType == ItemType.Melee)
        {
            if (meleeWeaponTemplatePrefab == null)
            {
                Instantiate(meleeWeaponTemplatePrefab, playerTransform);
            }

            Item meleetemplateWeaponItem = meleeWeaponTemplatePrefab.GetComponent<Item>();
            meleetemplateWeaponItem.AssignData(weapon);
        }

    }
}
