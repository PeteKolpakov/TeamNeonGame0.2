using Assets.Scripts.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioEventTrigger))]
public class Item : MonoBehaviour
{

    // projectile spawn point
    [SerializeField]
    private Transform _firePoint;

    [SerializeField]
    public ReworkedItem itemData;

    private float _attackTimer;

    public bool _isEquipped = false;

    private AudioEventTrigger _audio;

    public ItemType itemType { get => itemData.itemType; }
    public int price { get => itemData._price; }
    public Sprite icon { get => itemData._icon; }
    public string itemName { get => itemData._name; }
    public string itemDescription { get => itemData._description; }
    public int damage { get => itemData._damage; }
    public int projectileAmount { get => itemData._projectileAmount;set =>  itemData._projectileAmount = value; }
    public float fireRate { get => itemData._fireRate; set =>  itemData._fireRate = value;  }
    public float spreadAngle {get => itemData._spreadAngle; set => itemData._spreadAngle = value; }

    private void Awake()
    {
        _audio = GetComponent<AudioEventTrigger>();    
    }

    private void Update()
    {
        if (_attackTimer < itemData._fireRate)
        {
            _attackTimer += Time.deltaTime;
        }
    }

    public void Attack()
    {
        if (_attackTimer < itemData._fireRate)
            return;

        _audio.PlaySound();

        float angle = -itemData._spreadAngle / itemData._projectileAmount;
        float angleStep = (itemData._spreadAngle) / itemData._projectileAmount-1;
        for (int i = 0; i < itemData._projectileAmount; i++)
        {
            Instantiate(itemData._projectilePrefab, _firePoint.position, transform.rotation * Quaternion.Euler(0, 0, angle));
            angle += angleStep;
        }

        _attackTimer -= _attackTimer;
    }

    public void AssignData(ReworkedItem data)
    {
        itemData = data;
        GetComponent<SpriteRenderer>().color = data.colour;
    }
}
