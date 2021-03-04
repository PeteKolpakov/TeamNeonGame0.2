using SpriteGlow;
using System;
using UnityEngine;

public class SprialBulletShooting : MonoBehaviour
{
    [NonSerialized]
    public float Anlge = 90f;
    public GameObject BulletPrefab;
    public GameObject FirePoint;
    public float BulletSpeed = 6f;
    public float InvokeInterval = 0.2f;

    private BossRightArm _rightArm;
    private SpriteGlowEffect _glow;
    private Color _oldColor;
    private float _oldBrightness;
    private int _oldWidth;

    void Start()
    {
        _rightArm = GetComponent<BossRightArm>();
        _glow = GetComponent<SpriteGlowEffect>();

        _oldColor = _glow.GlowColor;
        _oldBrightness = _glow.GlowBrightness;
        _oldWidth = _glow.OutlineWidth;
    }

    private void Fire()
    {
        float bulletDirX = transform.position.x + Mathf.Sin((Anlge * Mathf.PI) / 180f);
        float bulletDirY = transform.position.y + Mathf.Cos((Anlge * Mathf.PI) / 180f);

        Vector3 bulletMoveVector = new Vector3(bulletDirX, bulletDirY, 0f);
        Vector2 bulletDirection = (bulletMoveVector - transform.position).normalized;

        GameObject bullet = Instantiate(BulletPrefab, FirePoint.transform.position, FirePoint.transform.rotation);

        Rigidbody2D _Bullet = bullet.GetComponent<Rigidbody2D>();

        _Bullet.AddForce(bulletDirection * BulletSpeed, ForceMode2D.Impulse);

        Anlge += 15f;
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void OnEnable()
    {
        InvokeRepeating("Fire", 0f, InvokeInterval);
    }

    public void ChangeInvokeParameters(float newInterval, float newBulletSpeed)
    {
        CancelInvoke();
        InvokeRepeating("Fire", 0f, newInterval);
        BulletSpeed = newBulletSpeed;
    }

    public void ResetInvokeParameters()
    {
        CancelInvoke();
        InvokeRepeating("Fire", 0f, 0.3f);
        BulletSpeed = 6f;

        _glow.GlowColor = _oldColor;
        _glow.GlowBrightness = _oldBrightness;
        _glow.OutlineWidth = _oldWidth;
        _rightArm.CanTakeDamage = true;
    }

    public void MakeInvincible()
    {
        _rightArm.CanTakeDamage = false;

        _glow.GlowColor = new Color(0, 51, 255, 255);
        _glow.GlowBrightness = 0.0525f;
        _glow.OutlineWidth = 1;
    }
}
