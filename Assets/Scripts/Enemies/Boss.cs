using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.EntityClass;
using UnityEngine.SceneManagement;
using Assets.Scripts.GameManager;
using SpriteGlow;


public class Boss : MonoBehaviour
{

    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    public BossLeftArm leftArm;
    public BossRightArm rightArm;
    public BossEyeball eyeball;

    public float amplitude;
    public float frequency;

    private WaveBulletShooting waveAttack;
    private SprialBulletShooting spiralAttack;
    private BossLaserBeamAttack laserBeam;
    public BossEyeTracker eyeTracker;
    public GameObject laserBeamGameObject;
    public GameObject smallEye;


    [NonSerialized]
    public bool leftArmDead;

    [NonSerialized]

    public bool rightArmDead;

    private bool stopDisabling = false;

    public GameObject EyeballHealthbar;
    public GameObject LeftArmHealthbar;
    public GameObject RightArmHealthbar;
    public GameObject FireRatePickup;
    private SpriteGlowEffect glow;
    private float oldBrightness;
    private int oldWidth;
    



    void Start()
    {
        posOffset = transform.position;
        waveAttack = leftArm.GetComponent<WaveBulletShooting>();
        spiralAttack = rightArm.GetComponent<SprialBulletShooting>();
        laserBeam = eyeball.GetComponent<BossLaserBeamAttack>();

        eyeball.canTakeDamage = false;

        // cashing eyeball glow
        glow = eyeball.GetComponent<SpriteGlowEffect>();
        oldBrightness = glow.GlowBrightness;
        oldWidth = glow.OutlineWidth;

        StartCoroutine(Shoot());
    }

    // Update is called once per frame
    void Update()
    {
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        transform.position = tempPos;

        if (laserBeam.attackFinished == false && leftArmDead == true && rightArmDead == true)
        {
            StopCoroutine(Shoot());
            EyeballHealthbar.SetActive(true);
            eyeball.canTakeDamage = true;
            laserBeam.enabled = true;
        }

        if (eyeball.health <= 0)
        {
            // Game over
            Time.timeScale = 0;

            StatsTracker stats = GameObject.FindGameObjectWithTag("GameManager").GetComponent<StatsTracker>();
            TimerUI timer = GameObject.FindGameObjectWithTag("GlobalUI").GetComponent<TimerUI>();

            stats.Timer = timer.timerText.text + timer.milisecondsText.text;

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        if (laserBeam.attackFinished == true && stopDisabling == false)
        {
            stopDisabling = true;
            laserBeam.enabled = false;
            laserBeamGameObject.SetActive(false);
            eyeTracker.enabled = true;
            StartCoroutine(Phase2Attack());
            laserBeam.StopAllCoroutines();
        }
    }

    public IEnumerator SpawnPickupables(){
        while(true){
            yield return new WaitForSeconds(8);
            float randomXOffset = UnityEngine.Random.Range(-9f,9f);
            Vector3 offset = new Vector3(randomXOffset,0,0);
            Instantiate(FireRatePickup,offset, Quaternion.identity );
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
            if (waveAttack != null)
                waveAttack.enabled = true;
            if (spiralAttack != null)
                spiralAttack.enabled = true;

            yield return new WaitForSeconds(10);
            // Phase 2 - Right arm enrage
            if (waveAttack != null)
                waveAttack.enabled = false;
            if (spiralAttack != null) // if the right arm exists - we enrage it
            {
                spiralAttack.enabled = true;
                RightArmHealthbar.SetActive(false);
                spiralAttack.MakeInvincible();
                spiralAttack.ChangeInvokeParameters(0.05f, 8f);
            }else if(waveAttack != null){  // otherwise, we enrage the left arm
                waveAttack.enabled = true;
                waveAttack.RotateTheSpiral();
                LeftArmHealthbar.SetActive(false);
                waveAttack.MakeInvincible();
                waveAttack.ChangeInvokeParameters(0.2f, 6f, 9);
                }

            yield return new WaitForSeconds(11);
            // Phase 3 - left arm enrage
            if (spiralAttack != null) // if the right arm exists we reset it after enrage
            {
                RightArmHealthbar.SetActive(true);
                spiralAttack.ResetInvokeParameters();
                spiralAttack.enabled = false;

                // if it exists, but the left arm doesn't - switch it to the default state
                if(waveAttack == null){
                    spiralAttack.enabled = true;
                }

            }else if(waveAttack != null){ // if the right arm still doesnt exist, we reset the enrage of the left arm
                LeftArmHealthbar.SetActive(true);
                waveAttack.ResetInvokeParameters();
                waveAttack.enabled = true; // and make it shoot in the default stage
            }
            yield return new WaitForSeconds(7);
            if (waveAttack != null)
            {
                LeftArmHealthbar.SetActive(true);
                waveAttack.ResetInvokeParameters();
                // Phase 4
                waveAttack.enabled = true;
            }
            if(spiralAttack != null)
                spiralAttack.enabled = false;
            
        }

    }

    private IEnumerator Phase2Attack()
    {
        StopCoroutine(Shoot());

        //lerp the glow
        glow.GlowBrightness = Mathf.Lerp(oldBrightness, 9.8f, 2f);
        glow.OutlineWidth = 3;

        yield return new WaitForSeconds(2);
        var randomLeft = UnityEngine.Random.Range(-9.7f, 0.5f);
        var randomRight = UnityEngine.Random.Range(0.5f, 10.3f);
        Vector3 posLeft = new Vector3(randomLeft, 8f, 0);
        Vector3 posRight = new Vector3(randomRight, 8f, 0);

        var leftEye = Instantiate(smallEye, posLeft, Quaternion.identity).GetComponent<SmallEyeballs>();
        var rightEye = Instantiate(smallEye, posRight, Quaternion.identity).GetComponent<SmallEyeballs>();

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
                if(leftEye.AfterImage != null){
                leftEye.AfterImage.Play();
            }
                rightEye.transform.position = Vector3.Lerp(rightEye.transform.position, posRight, time / 0.3f);
                if(rightEye.AfterImage != null){
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
