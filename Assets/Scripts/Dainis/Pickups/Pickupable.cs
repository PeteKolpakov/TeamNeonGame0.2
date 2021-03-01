using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupable : MonoBehaviour
{
    public PickupableType pickupableType;

    public delegate void PickupCurrency(int currency);
    public static event PickupCurrency pickupCurrency;

    public delegate void PickupAmmo(int ammo);
    public static event PickupAmmo pickupAmmo;

    public enum PickupableType
    {
        Currency,
        AmmoPickup
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if(pickupableType == PickupableType.Currency)
            {
                int amount = Random.Range(2, 7);
                pickupCurrency(amount);
                Destroy(gameObject);
            }else if(pickupableType == PickupableType.AmmoPickup)
            {
                int amount = Random.Range(1, 3);
                Destroy(gameObject);
                pickupAmmo(amount);

            }
        }
    }
}
