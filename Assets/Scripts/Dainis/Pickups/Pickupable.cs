using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupable : MonoBehaviour
{
    public PickupableType pickupableType;

    public delegate void PickupCurrency(int currency);
    public static event PickupCurrency pickupCurrency;

    public delegate void PickupFireRate(float firerate);
    public static event PickupFireRate pickupFR;

    public enum PickupableType
    {
        Currency,
        FireRatePickup
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
            }else if(pickupableType == PickupableType.FireRatePickup)
            {
                // Add firerate
                float amount = Random.Range(0.1f, 0.2f);
                pickupFR(amount);
                Destroy(gameObject);

            }
        }
    }
}
