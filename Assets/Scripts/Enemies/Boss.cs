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

    [NonSerialized]
    public bool leftArmDead;

    [NonSerialized]

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
        while (true)
        {
            yield return new WaitForSeconds(1);
            // Phase 1 
                if(waveAttack != null)
                    waveAttack.enabled = true;
                if(spiralAttack != null)
                    spiralAttack.enabled = true;

            yield return new WaitForSeconds(10);
           // Phase 2
                if(waveAttack != null)
                    waveAttack.enabled = false;
                if(spiralAttack != null){
                    spiralAttack.enabled = true;

                    spiralAttack.MakeInvincible();
                    spiralAttack.ChangeInvokeParameters(0.05f, 8f);
                }

            yield return new WaitForSeconds(11);
            if(spiralAttack != null){
                spiralAttack.ResetInvokeParameters();
            }
            // Phase 3
                if(waveAttack != null){
                    waveAttack.enabled = true;
                    waveAttack.RotateTheSpiral();
                    waveAttack.MakeInvincible();
                    waveAttack.ChangeInvokeParameters(0.2f, 6f, 9);          
                }
                if(spiralAttack != null)
                    spiralAttack.enabled = false;


            yield return new WaitForSeconds(11);
            if(waveAttack != null)
                waveAttack.ResetInvokeParameters();
            // Phase 4
                waveAttack.enabled = false;
                spiralAttack.enabled = false;
        }

    }
}
