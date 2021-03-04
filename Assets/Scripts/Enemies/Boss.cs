using SpriteGlow;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Boss : MonoBehaviour
{

    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    public BossLeftArm LeftArm;
    public BossRightArm RightArm;
    public BossEyeball Eyeball;

    public float Amplitude;
    public float Frequency;

    private WaveBulletShooting _waveAttack;
    private SprialBulletShooting _spiralAttack;
    private BossLaserBeamAttack _laserBeam;
    public BossEyeTracker EyeTracker;
    public GameObject LaserBeamGameObject;
    public GameObject SmallEye;


    [NonSerialized]
    public bool LeftArmDead;

    [NonSerialized]

    public bool RightArmDead;

    private bool _stopDisabling = false;

    public GameObject EyeballHealthbar;
    public GameObject LeftArmHealthbar;
    public GameObject RightArmHealthbar;
    public GameObject FireRatePickup;
    private SpriteGlowEffect _glow;
    private float _oldBrightness;
    private int _oldWidth;


    void Start()
    {
        posOffset = transform.position;
        _waveAttack = LeftArm.GetComponent<WaveBulletShooting>();
        _spiralAttack = RightArm.GetComponent<SprialBulletShooting>();
        _laserBeam = Eyeball.GetComponent<BossLaserBeamAttack>();

        Eyeball.CanTakeDamage = false;

        // cashing eyeball glow
        _glow = Eyeball.GetComponent<SpriteGlowEffect>();
        _oldBrightness = _glow.GlowBrightness;
        _oldWidth = _glow.OutlineWidth;

        StartCoroutine(Shoot());
    }

    void Update()
    {
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * Frequency) * Amplitude;

        transform.position = tempPos;

        if (_laserBeam.AttackFinished == false && LeftArmDead == true && RightArmDead == true)
        {
            StopCoroutine(Shoot());
            EyeballHealthbar.SetActive(true);
            Eyeball.CanTakeDamage = true;
            _laserBeam.enabled = true;
        }

        if (Eyeball.Health <= 0)
        {
            // Game over
            Time.timeScale = 0;

            StatsTracker stats = GameObject.FindGameObjectWithTag("GameManager").GetComponent<StatsTracker>();
            TimerUI timer = GameObject.FindGameObjectWithTag("GlobalUI").GetComponent<TimerUI>();

            stats.Timer = timer.timerText.text + timer.milisecondsText.text;

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        if (_laserBeam.AttackFinished == true && _stopDisabling == false)
        {
            _stopDisabling = true;
            _laserBeam.enabled = false;
            LaserBeamGameObject.SetActive(false);
            EyeTracker.enabled = true;
            StartCoroutine(Phase2Attack());
            _laserBeam.StopAllCoroutines();
        }
    }

    public IEnumerator SpawnPickupables()
    {
        while (true)
        {
            yield return new WaitForSeconds(8);
            float randomXOffset = UnityEngine.Random.Range(-9f, 9f);
            Vector3 offset = new Vector3(randomXOffset, 0, 0);
            Instantiate(FireRatePickup, offset, Quaternion.identity);
        }
    }

    private IEnumerator Shoot()
    {
        StartCoroutine(SpawnPickupables());
        yield return new WaitForSeconds(3);
        while (true)
        {
            yield return new WaitForSeconds(1);
            // Phase 1 - default
            if (_waveAttack != null)
                _waveAttack.enabled = true;
            if (_spiralAttack != null)
                _spiralAttack.enabled = true;

            yield return new WaitForSeconds(10);
            // Phase 2 - Right arm enrage
            if (_waveAttack != null)
                _waveAttack.enabled = false;
            if (_spiralAttack != null) // if the right arm exists - we enrage it
            {
                _spiralAttack.enabled = true;
                RightArmHealthbar.SetActive(false);
                _spiralAttack.MakeInvincible();
                _spiralAttack.ChangeInvokeParameters(0.05f, 8f);
            }
            else if (_waveAttack != null)
            {  // otherwise, we enrage the left arm
                _waveAttack.enabled = true;
                _waveAttack.RotateTheSpiral();
                LeftArmHealthbar.SetActive(false);
                _waveAttack.MakeInvincible();
                _waveAttack.ChangeInvokeParameters(0.2f, 6f, 9);
            }

            yield return new WaitForSeconds(11);
            // Phase 3 - left arm enrage
            if (_spiralAttack != null) // if the right arm exists we reset it after enrage
            {
                RightArmHealthbar.SetActive(true);
                _spiralAttack.ResetInvokeParameters();
                _spiralAttack.enabled = false;

                // if it exists, but the left arm doesn't - switch it to the default state
                if (_waveAttack == null)
                {
                    _spiralAttack.enabled = true;
                }

            }
            else if (_waveAttack != null)
            { // if the right arm still doesnt exist, we reset the enrage of the left arm
                LeftArmHealthbar.SetActive(true);
                _waveAttack.ResetInvokeParameters();
                _waveAttack.enabled = true; // and make it shoot in the default stage
            }
            yield return new WaitForSeconds(7);
            if (_waveAttack != null)
            {
                LeftArmHealthbar.SetActive(true);
                _waveAttack.ResetInvokeParameters();
                // Phase 4
                _waveAttack.enabled = true;
            }
            if (_spiralAttack != null)
                _spiralAttack.enabled = false;

        }

    }

    private IEnumerator Phase2Attack()
    {
        StopCoroutine(Shoot());

        //lerp the glow
        _glow.GlowBrightness = Mathf.Lerp(_oldBrightness, 9.8f, 2f);
        _glow.OutlineWidth = 3;

        yield return new WaitForSeconds(2);
        var randomLeft = UnityEngine.Random.Range(-9.7f, 0.5f);
        var randomRight = UnityEngine.Random.Range(0.5f, 10.3f);
        Vector3 posLeft = new Vector3(randomLeft, 8f, 0);
        Vector3 posRight = new Vector3(randomRight, 8f, 0);

        var leftEye = Instantiate(SmallEye, posLeft, Quaternion.identity).GetComponent<SmallEyeballs>();
        var rightEye = Instantiate(SmallEye, posRight, Quaternion.identity).GetComponent<SmallEyeballs>();

        while (true)
        {

            randomLeft = UnityEngine.Random.Range(-9.7f, 0.5f);
            randomRight = UnityEngine.Random.Range(0.5f, 10.3f);
            posLeft = new Vector3(randomLeft, 8f, 0);
            posRight = new Vector3(randomRight, 8f, 0);

            float time = 0;
            while (time < 0.3f)
            {
                leftEye.transform.position = Vector3.Lerp(leftEye.transform.position, posLeft, time / 0.3f);
                if (leftEye.AfterImage != null)
                {
                    leftEye.AfterImage.Play();
                }
                rightEye.transform.position = Vector3.Lerp(rightEye.transform.position, posRight, time / 0.3f);
                if (rightEye.AfterImage != null)
                {
                    rightEye.AfterImage.Play();
                }
                yield return null;
                time += Time.deltaTime;
            }

            leftEye.FireBeam();
            rightEye.FireBeam();
            yield return new WaitForSeconds(1.6f);
        }
    }
}
