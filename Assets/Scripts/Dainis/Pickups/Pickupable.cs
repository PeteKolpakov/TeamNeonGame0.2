using Assets.Scripts.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioEventTrigger))]
public class Pickupable : MonoBehaviour
{
    public PickupableType pickupableType;

    public delegate void PickupCurrency(int currency);
    public static event PickupCurrency pickupCurrency;

    public delegate void PickupFireRate(float firerate);
    public static event PickupFireRate pickupFR;

    private AudioEventTrigger _audio;

    private void Awake()
    {
        _audio = GetComponent<AudioEventTrigger>();
    }
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
                _audio.PlaySound();
                int amount = Random.Range(2, 7);
                pickupCurrency(amount);
                Destroy(gameObject);
            }else if(pickupableType == PickupableType.FireRatePickup)
            {
                _audio.PlaySound();
                // Add firerate
                float amount = Random.Range(0.1f, 0.2f);
                pickupFR(amount);
                Destroy(gameObject);

            }
        }
    }
}
