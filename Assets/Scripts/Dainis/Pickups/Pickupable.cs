using Assets.Scripts.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.GameManager;

[RequireComponent(typeof(AudioEventTrigger))]
public class Pickupable : MonoBehaviour
{
    public PickupableType pickupableType;

    public delegate void PickupScore(int score);
    public static event PickupScore pickupScore;

    public delegate void PickupFireRate(float firerate);
    public static event PickupFireRate pickupFR;

    private AudioEventTrigger _audio;


    private void Awake()
    {
        _audio = GetComponent<AudioEventTrigger>();
        Destroy(gameObject, 5f);
    }
    public enum PickupableType
    {
        Score,
        FireRatePickup
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if(pickupableType == PickupableType.Score)
            {
                _audio.PlaySound();
                pickupScore(100);
                Destroy(gameObject);
            }else if(pickupableType == PickupableType.FireRatePickup)
            {
                _audio.PlaySound();
                // Add firerate
                float amount = Random.Range(0.1f, 0.2f);
                PlayerStatManager player = PlayerTracker.Instance.Player.GetComponent<PlayerStatManager>();
                player.AddFireRate(amount);
                Destroy(gameObject);

            }
        }
    }
}
