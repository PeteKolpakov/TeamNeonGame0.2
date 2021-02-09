using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item2 : ScriptableObject
{
    // Name
    public string _name;
    // Description
    public string _description;
    // Price
    public int _price;

    public int _damage = 1;
    public ItemType itemType;
    public float _fireRate = 1f;


    private float _attackTimer;
    // projectile amount

    public int _projectileAmount = 1;
    // spread angle

    public float _spreadAngle = 5;
    // Sprite
    public Sprite _icon;
    // projectile spawn point
    [SerializeField]
    private Transform _firePoint;
    // Bullet prefab
    [SerializeField]
    private GameObject _projectilePrefab;
    public bool _isEquipped = false;

    public enum ItemType
    {
        Ranged,
        Melee,
        Consumable
    }
}

   /* private void Update()
    {
        if (_attackTimer < _fireRate)
        {
            _attackTimer += Time.deltaTime;
        }
    }
}*/

/*   public void Attack()
    {
        // ammo check

        if (_attackTimer < _fireRate)
            return;

        for (int i = 0; i < _projectileAmount; i++)
        {
            float angle = Random.Range(-_spreadAngle, _spreadAngle);
            Instantiate(_projectilePrefab, _firePoint.position, transform.rotation * Quaternion.Euler(0, 0, angle));
        }
        _attackTimer -= _attackTimer;
    }*/