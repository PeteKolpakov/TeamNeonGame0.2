using SpriteGlow;
using System.Collections;
using UnityEngine;

public class WaveBulletShooting : MonoBehaviour
{
    [SerializeField]
    private int _bulletAmount = 10;

    [SerializeField]
    private float _startAngle = 90f, _endAngle = 270;

    public GameObject FirePoint;
    public GameObject BulletPrefab;
    public float BulletSpeed = 5f;
    public float InvokeInterval = 2f;

    private BossLeftArm _leftArm;
    private SpriteGlowEffect _glow;
    private Color _oldColor;
    private float _oldBrightness;
    private int _oldWidth;
    private bool _waveRotationActive = false;

    private void Start()
    {
        _leftArm = GetComponent<BossLeftArm>();
        _glow = GetComponent<SpriteGlowEffect>();

        _oldColor = _glow.GlowColor;
        _oldBrightness = _glow.GlowBrightness;
        _oldWidth = _glow.OutlineWidth;
    }

    private void Fire()
    {
        float angleStep = (_endAngle - _startAngle) / _bulletAmount;
        float angle = _startAngle;

        for (int i = 0; i < _bulletAmount; i++)
        {
            float bulletDirX = FirePoint.transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float bulletDirY = FirePoint.transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

            Vector3 bulletMoveVector = new Vector3(bulletDirX, bulletDirY, 0f);
            Vector2 bulletDirection = (bulletMoveVector - FirePoint.transform.position).normalized;

            GameObject bullet = Instantiate(BulletPrefab, FirePoint.transform.position, FirePoint.transform.rotation);

            Rigidbody2D _Bullet = bullet.GetComponent<Rigidbody2D>();

            _Bullet.AddForce(bulletDirection * BulletSpeed, ForceMode2D.Impulse);

            angle += angleStep;
        }
    }

    private void OnEnable()
    {
        InvokeRepeating("Fire", 0f, InvokeInterval);
    }

    public void ChangeInvokeParameters(float newInterval, float newBulletSpeed, int newBulletAmount)
    {
        CancelInvoke();
        InvokeRepeating("Fire", 0f, newInterval);
        _bulletAmount = newBulletAmount;
        BulletSpeed = newBulletSpeed;
    }

    public void ResetInvokeParameters()
    {
        CancelInvoke();
        _waveRotationActive = false;
        StopCoroutine(AngleRotation());


        InvokeRepeating("Fire", 0f, 2f);
        _bulletAmount = 12;
        BulletSpeed = 5f;
        _startAngle = 90;
        _endAngle = 270;

        _glow.GlowColor = _oldColor;
        _glow.GlowBrightness = _oldBrightness;
        _glow.OutlineWidth = _oldWidth;
        _leftArm.CanTakeDamage = true;

    }

    public void RotateTheSpiral()
    {
        _waveRotationActive = true;
        StartCoroutine(AngleRotation());
    }

    private IEnumerator AngleRotation()
    {
        while (_waveRotationActive == true)
        {
            _startAngle += 0.8f;
            _endAngle += 0.8f;
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void MakeInvincible()
    {
        _leftArm.CanTakeDamage = false;

        _glow.GlowColor = new Color(0, 51, 255, 255);
        _glow.GlowBrightness = 0.0525f;
        _glow.OutlineWidth = 1;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
        CancelInvoke();
    }
    private void OnDisable()
    {
        CancelInvoke();
        StopAllCoroutines();
    }

}


