using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.EntityClass;


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
    public bool leftArmDead = false;
    [NonSerialized]

    public bool rightArmDead = false;

    private bool _phase1;



    void Start()
    {
        posOffset = transform.position;
        waveAttack = leftArm.GetComponent<WaveBulletShooting>();
        spiralAttack = rightArm.GetComponent<SprialBulletShooting>();

        eyeball.GetComponent<EnemyBase>().canTakeDamage = false;

        StartCoroutine(Shoot());
    }

    // Update is called once per frame
    void Update()
    {
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        transform.position = tempPos;

        if(leftArmDead == true && rightArmDead == true){
            eyeball.GetComponent<EnemyBase>().canTakeDamage = true;
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
