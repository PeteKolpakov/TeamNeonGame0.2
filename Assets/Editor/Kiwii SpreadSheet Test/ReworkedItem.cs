using UnityEngine;

// TEMPORARY TEST CLASS

[CreateAssetMenu(fileName = "New Item", menuName = "Assets/Resources/Test")]
public class ReworkedItem : ScriptableObject
{
    // Name
    public string _name;
    // Description
    public string _description;
    // Price
    public int _price;

    public int _damage;
    public ItemType itemType;
    public float _fireRate;


    private float _attackTimer;
    // projectile amount

    public int _projectileAmount;
    // spread angle

    public float _spreadAngle;
    // Sprite
    public Sprite _icon;
    // projectile spawn point
    [SerializeField]
    private Transform _firePoint;
    // Bullet prefab
    [SerializeField]
    private GameObject _projectilePrefab;
    public bool _isEquipped = false;


    public delegate void RemoveAmmo(int ammo);
    public static event RemoveAmmo removeAmmo;

    public enum ItemType
    {
        Ranged,
        Melee,
        Consumable
    }
}