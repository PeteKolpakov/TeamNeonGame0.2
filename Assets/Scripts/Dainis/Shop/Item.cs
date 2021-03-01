using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{

    // projectile spawn point
    [SerializeField]
    private Transform _firePoint;

    [SerializeField]
    public ReworkedItem itemData;

    private float _attackTimer;

    public bool _isEquipped = false;


    public ItemType itemType { get => itemData.itemType; }
    public int price { get => itemData._price; }
    public Sprite icon { get => itemData._icon; }
    public string itemName { get => itemData._name; }
    public string itemDescription { get => itemData._description; }
    public int damage { get => itemData._damage; }
    public int projectileAmount { get => itemData._projectileAmount; }
    public float fireRate { get => itemData._fireRate;  }


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

        for (int i = 0; i < itemData._projectileAmount; i++)
        {
            float angle = Random.Range(-itemData._spreadAngle, itemData._spreadAngle);
            Instantiate(itemData._projectilePrefab, _firePoint.position, transform.rotation * Quaternion.Euler(0, 0, angle));
        }

        _attackTimer -= _attackTimer;

    }

    public void AssignData(ReworkedItem data)
    {
        itemData = data;
        GetComponent<SpriteRenderer>().color = data.colour;
    }
}
