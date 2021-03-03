using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpriteGlow;

public class WaveBulletShooting : MonoBehaviour
{
    [SerializeField]
    private int _bulletAmount = 10;

    [SerializeField]
    private float _startAngle = 90f, _endAngle = 270;

    public GameObject firePoint;
    public GameObject bulletPrefab;
    public float _bulletSpeed = 5f;
    public float invokeInterval = 2f;

    private BossLeftArm leftArm;
    private SpriteGlowEffect glow;
    private Color oldColor;
    private float oldBrightness;
    private int oldWidth;
    private bool _waveRotationActive = false;

    private void Start() {
        leftArm = GetComponent<BossLeftArm>();
        glow = GetComponent<SpriteGlowEffect>();

        oldColor = glow.GlowColor;
        oldBrightness = glow.GlowBrightness;
        oldWidth = glow.OutlineWidth;
    }

    private void Fire()
    {
        float angleStep = (_endAngle - _startAngle) / _bulletAmount;
        float angle = _startAngle;

        for (int i = 0; i < _bulletAmount; i++)
        {
            float bulletDirX = firePoint.transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float bulletDirY = firePoint.transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

            Vector3 bulletMoveVector = new Vector3(bulletDirX, bulletDirY, 0f);
            Vector2 bulletDirection = (bulletMoveVector - firePoint.transform.position).normalized;

            GameObject bullet = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);

            Rigidbody2D _Bullet = bullet.GetComponent<Rigidbody2D>();

            _Bullet.AddForce(bulletDirection * _bulletSpeed, ForceMode2D.Impulse);

            angle += angleStep;
        }
    }

    private void OnEnable()
    {
        InvokeRepeating("Fire", 0f, invokeInterval);
    }

    public void ChangeInvokeParameters(float newInterval, float newBulletSpeed, int newBulletAmount){
        CancelInvoke();
        InvokeRepeating("Fire", 0f, newInterval);
        _bulletAmount = newBulletAmount;
        _bulletSpeed = newBulletSpeed;
    }

    public void ResetInvokeParameters(){
        CancelInvoke();
        _waveRotationActive = false;
        StopCoroutine(AngleRotation());


        InvokeRepeating("Fire", 0f, 2f);
        _bulletAmount = 12;
        _bulletSpeed = 5f;
        _startAngle = 90;
        _endAngle = 270;

        glow.GlowColor = oldColor;
        glow.GlowBrightness = oldBrightness;
        glow.OutlineWidth = oldWidth;
        leftArm.canTakeDamage = true;
    
    }

    public void RotateTheSpiral(){
        _waveRotationActive = true;
        StartCoroutine(AngleRotation());
    }

    private IEnumerator AngleRotation(){
        while(_waveRotationActive == true){
            _startAngle += 0.8f;
            _endAngle += 0.8f;
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void MakeInvincible(){
        leftArm.canTakeDamage = false;

        glow.GlowColor = new Color(0,51,255,255);
        glow.GlowBrightness = 0.0525f;
        glow.OutlineWidth = 1;
    }

    private void OnDestroy() {
        StopAllCoroutines();
        CancelInvoke();
    }
    private void OnDisable()
    {
        CancelInvoke();
        StopAllCoroutines();
    }

}


