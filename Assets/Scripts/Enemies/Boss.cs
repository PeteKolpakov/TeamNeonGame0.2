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

    public bool leftArmDead;

    public bool rightArmDead;

    private bool _phase1;



    void Start()
    {
        posOffset = transform.position;
        waveAttack = leftArm.GetComponent<WaveBulletShooting>();
        spiralAttack = rightArm.GetComponent<SprialBulletShooting>();

        eyeball.canTakeDamage = false;

        StartCoroutine(Shoot());
    }

    // Update is called once per frame
    void Update()
    {
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        transform.position = tempPos;

        if(leftArmDead == true && rightArmDead == true){
            eyeball.canTakeDamage = true;
        }

        if(eyeball.health <= 0){
            // Game over
            Time.timeScale = 0;

            StatsTracker stats = GameObject.FindGameObjectWithTag("GameManager").GetComponent<StatsTracker>();
            TimerUI timer  = GameObject.FindGameObjectWithTag("GlobalUI").GetComponent<TimerUI>();

            stats.Timer = timer.timerText.text + timer.milisecondsText.text;
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private IEnumerator Shoot()
    {
        while (_phase1 == true)
        {
            yield return new WaitForSeconds(1);
            if(waveAttack != null)
                waveAttack.enabled = true;
            yield return new WaitForSeconds(10);
            if (waveAttack != null)
                waveAttack.enabled = false;
            if (spiralAttack != null)
                spiralAttack.enabled = true;
            yield return new WaitForSeconds(11);
            if (waveAttack != null)
                waveAttack.enabled = true;
            if (spiralAttack != null)
                spiralAttack.enabled = true;
            yield return new WaitForSeconds(11);
            if (waveAttack != null)
                waveAttack.enabled = false;
            if (spiralAttack != null)
                spiralAttack.enabled = false;
        }

    }
}
