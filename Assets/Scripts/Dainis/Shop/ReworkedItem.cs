using UnityEngine;

// TEMPORARY TEST CLASS

[CreateAssetMenu(fileName = "New Item", menuName = "Assets/Resources/Test")]
public class ReworkedItem : ScriptableObject
{

    public string _name;
    public string _description;
    public int _price;
    public int _damage;
    public ItemType itemType;
    public float _fireRate;
    public int _projectileAmount;
    public float _spreadAngle;
    public int WeaponID;
    public Sprite _icon;
    [SerializeField]
    public GameObject _projectilePrefab;
    public bool _isEquipped = false;
    public Color colour;


    //public delegate void RemoveAmmo(int ammo);
    //public static event RemoveAmmo removeAmmo;

}
    public enum ItemType
    {
        Ranged,
        Melee,
        Consumable
    }