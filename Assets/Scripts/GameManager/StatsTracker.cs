using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsTracker : MonoBehaviour
{
    public string Timer;
    public int DeathCount = 0;
    public int EnemiesKilled = 0;

    private void Awake() {
        DontDestroyOnLoad (transform.gameObject);
    }
}
