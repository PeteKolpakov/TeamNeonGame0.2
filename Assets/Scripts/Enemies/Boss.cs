using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.EntityClass;

using UnityEngine.SceneManagement;


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





    void Start()
    {
        posOffset = transform.position;
        waveAttack = leftArm.GetComponent<WaveBulletShooting>();
        spiralAttack = rightArm.GetComponent<SprialBulletShooting>();
        laserBeam = eyeball.GetComponent<BossLaserBeamAttack>();

        eyeball.canTakeDamage = false;

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
            StopAllCoroutines();
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

    private IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            // Phase 1 
            if (waveAttack != null)
                waveAttack.enabled = true;
            if (spiralAttack != null)
                spiralAttack.enabled = true;

            yield return new WaitForSeconds(10);
            // Phase 2
            if (waveAttack != null)
                waveAttack.enabled = false;
            if (spiralAttack != null)
            {
                spiralAttack.enabled = true;

                spiralAttack.MakeInvincible();
                spiralAttack.ChangeInvokeParameters(0.05f, 8f);
            }

            yield return new WaitForSeconds(11);
            if (spiralAttack != null)
            {
                spiralAttack.ResetInvokeParameters();
            }
            // Phase 3
            if (waveAttack != null)
            {
                waveAttack.enabled = true;
                waveAttack.RotateTheSpiral();
                waveAttack.MakeInvincible();
                waveAttack.ChangeInvokeParameters(0.2f, 6f, 9);
            }
            if (spiralAttack != null)
                spiralAttack.enabled = false;

            yield return new WaitForSeconds(11);
            if (waveAttack != null)
            {
                waveAttack.ResetInvokeParameters();
                // Phase 4
                waveAttack.enabled = true;
                spiralAttack.enabled = false;
            }
        }

    }

    private IEnumerator Phase2Attack()
    {
        StopCoroutine(Shoot());
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
                rightEye.transform.position = Vector3.Lerp(rightEye.transform.position, posRight, time / 0.3f);
                yield return null;
                time += Time.deltaTime;
            }

            leftEye.FireBeam();
            rightEye.FireBeam();
            yield return new WaitForSeconds(1.6f);
        }

    }
}
