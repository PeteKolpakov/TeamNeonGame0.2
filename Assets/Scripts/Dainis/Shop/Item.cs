using Assets.Scripts.Audio;
using UnityEngine;

[RequireComponent(typeof(AudioEventTrigger))]
public class Item : MonoBehaviour
{

    // projectile spawn point
    [SerializeField]
    private Transform _firePoint;

    [SerializeField]
    public ReworkedItem ItemData;

    private float _attackTimer;

    public bool IsEquipped = false;

    private AudioEventTrigger _audio;

    public ItemType itemType { get => ItemData.itemType; }
    public int price { get => ItemData._price; }
    public Sprite icon { get => ItemData._icon; }
    public string itemName { get => ItemData._name; }
    public string itemDescription { get => ItemData._description; }
    public int damage { get => ItemData._damage; }
    public int projectileAmount { get => ItemData._projectileAmount; set => ItemData._projectileAmount = value; }
    public float fireRate { get => ItemData._fireRate; set => ItemData._fireRate = value; }
    public float spreadAngle { get => ItemData._spreadAngle; set => ItemData._spreadAngle = value; }

    private void Awake()
    {
        _audio = GetComponent<AudioEventTrigger>();
    }

    private void Update()
    {
        if (_attackTimer < ItemData._fireRate)
        {
            _attackTimer += Time.deltaTime;
        }
    }

    public void Attack()
    {
        if (_attackTimer < ItemData._fireRate)
            return;

        _audio.PlaySound();

        float angle = -ItemData._spreadAngle / ItemData._projectileAmount;
        float angleStep = (ItemData._spreadAngle) / ItemData._projectileAmount - 1;
        for (int i = 0; i < ItemData._projectileAmount; i++)
        {
            Instantiate(ItemData._projectilePrefab, _firePoint.position, transform.rotation * Quaternion.Euler(0, 0, angle));
            angle += angleStep;
        }
        _attackTimer -= _attackTimer;
    }

    public void AssignData(ReworkedItem data)
    {
        ItemData = data;
        GetComponent<SpriteRenderer>().color = data.colour;
    }
}
