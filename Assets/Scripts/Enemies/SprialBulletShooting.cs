using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SpriteGlow;

public class SprialBulletShooting : MonoBehaviour
{
    [NonSerialized]
    public float angle = 90f;
    public GameObject bulletPrefab;
    public GameObject firePoint;
    public float _bulletSpeed = 6f;
    public float invokeInterval = 0.2f;

    private BossRightArm rightArm;
    private SpriteGlowEffect glow;
    private Color oldColor;
    private float oldBrightness;
    private int oldWidth;
    
    void Start()
    {
        rightArm = GetComponent<BossRightArm>();
        glow = GetComponent<SpriteGlowEffect>();

        oldColor = glow.GlowColor;
        oldBrightness = glow.GlowBrightness;
        oldWidth = glow.OutlineWidth;
    }

    private void Fire()
    {
        float bulletDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
        float bulletDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

        Vector3 bulletMoveVector = new Vector3(bulletDirX, bulletDirY, 0f);
        Vector2 bulletDirection = (bulletMoveVector - transform.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);

        Rigidbody2D _Bullet = bullet.GetComponent<Rigidbody2D>();

        _Bullet.AddForce(bulletDirection * _bulletSpeed, ForceMode2D.Impulse);

        angle += 15f;
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void OnEnable()
    {
        InvokeRepeating("Fire", 0f, invokeInterval);
    }

    public void ChangeInvokeParameters(float newInterval, float newBulletSpeed){
        CancelInvoke();
        InvokeRepeating("Fire", 0f, newInterval);
        _bulletSpeed = newBulletSpeed;
    }

    public void ResetInvokeParameters(){
        CancelInvoke();
        InvokeRepeating("Fire", 0f, 0.3f);
        _bulletSpeed = 6f;

        glow.GlowColor = oldColor;
        glow.GlowBrightness = oldBrightness;
        glow.OutlineWidth = oldWidth;
        rightArm.canTakeDamage = true;
    }

    public void MakeInvincible(){
        rightArm.canTakeDamage = false;

        glow.GlowColor = new Color(0,51,255,255);
        glow.GlowBrightness = 0.0525f;
        glow.OutlineWidth = 1;
    }
}
