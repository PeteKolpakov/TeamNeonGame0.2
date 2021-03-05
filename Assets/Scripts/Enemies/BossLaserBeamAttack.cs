using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLaserBeamAttack : MonoBehaviour
{
    public GameObject LaserBeam;
    public BossEyeTracker EyeTracker;
    public GameObject TheEye;

    Quaternion startRotation;
    Quaternion endRotation;
    public float RotationProgress = -1f;
    private bool _changeDirection;
    public bool AttackFinished = false;
    public GameObject FlyingEnemyPrefab;
    public List<Transform> SpawnPointsForEnemies;
    private void OnEnable()
    {
        EyeTracker.enabled = false;
        LaserBeam.SetActive(true);
        TheEye.transform.rotation = Quaternion.Euler(0, 0, 40);
        StartRotating();
    }

    public void StartRotating()
    {
        startRotation = Quaternion.Euler(0, 0, 40);
        endRotation = Quaternion.Euler(0, 0, -40);

        RotationProgress = 0;
        StartCoroutine(SpawnFlyingEnemiesCoroutine());
    }

    private void Update()
    {
        if (AttackFinished == false && _changeDirection == false && RotationProgress < 1 && RotationProgress >= 0)
        {
            RotationProgress += Time.deltaTime * 0.1f;
            TheEye.transform.rotation = Quaternion.Lerp(startRotation, endRotation, RotationProgress);
        }
        else if (AttackFinished == false && RotationProgress > 1)
        {
            _changeDirection = true;
        }
        if (AttackFinished == false && _changeDirection == true && RotationProgress < 2 && RotationProgress >= 0)
        {
            RotationProgress -= Time.deltaTime * 0.1f;
            TheEye.transform.rotation = Quaternion.Lerp(startRotation, endRotation, RotationProgress);
        }

        if (AttackFinished == false && _changeDirection == true && RotationProgress < 0)
        {
            AttackFinished = true;
        }
    }

    private IEnumerator SpawnFlyingEnemiesCoroutine()
    {
        while (true)
        {
            SpawnEnemies();
            yield return new WaitForSeconds(0.4f);
        }
    }

    private void SpawnEnemies()
    {
        int k = Random.Range(0, SpawnPointsForEnemies.Count);
        Instantiate(FlyingEnemyPrefab, SpawnPointsForEnemies[k].transform.position, Quaternion.identity);
    }
}
