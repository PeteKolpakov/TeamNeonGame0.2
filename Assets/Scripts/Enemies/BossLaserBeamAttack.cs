using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.GameManager;

public class BossLaserBeamAttack : MonoBehaviour
{
    public GameObject laserBeam;
    public BossEyeTracker eyeTracker;
    public GameObject theEye;

    Quaternion startRotation;
    Quaternion endRotation;
    public float rotationProgress = -1f;
    private bool _changeDirection;
    public bool attackFinished = false;
    public GameObject flyingEnemyPrefab;
    public List<Transform> SpawnPointsForEnemies;
    private void OnEnable() {
        eyeTracker.enabled = false;
        laserBeam.SetActive(true);
        theEye.transform.rotation = Quaternion.Euler(0,0,40);

        StartRotating();
    }

    public void StartRotating(){
        startRotation = Quaternion.Euler(0,0,40);
        endRotation = Quaternion.Euler(0,0,-40);

        rotationProgress = 0;
        StartCoroutine(SpawnFlyingEnemiesCoroutine());
    }

    private void Update() {
        if (attackFinished == false && _changeDirection == false && rotationProgress < 1 && rotationProgress >= 0){
            rotationProgress += Time.deltaTime * 0.1f;
            theEye.transform.rotation = Quaternion.Lerp(startRotation, endRotation, rotationProgress);
        }else if(attackFinished == false && rotationProgress >1){
            _changeDirection = true;
        }
        if(attackFinished == false && _changeDirection == true && rotationProgress <2 && rotationProgress >= 0){
            rotationProgress -= Time.deltaTime * 0.1f;
            theEye.transform.rotation = Quaternion.Lerp(startRotation, endRotation, rotationProgress);
        }

        if(attackFinished == false && _changeDirection == true && rotationProgress <0){
            attackFinished = true;
        }

    }

    private IEnumerator SpawnFlyingEnemiesCoroutine(){
        while(true){
            SpawnEnemies();
            yield return new WaitForSeconds(0.4f);

        }
    }

    private void SpawnEnemies(){
            int k = Random.Range(0,SpawnPointsForEnemies.Count);
            Instantiate(flyingEnemyPrefab, SpawnPointsForEnemies[k].transform.position, Quaternion.identity);
    }
}
