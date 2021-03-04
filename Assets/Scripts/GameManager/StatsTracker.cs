using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsTracker : MonoBehaviour
{

    public bool SpeedrunMode;
    public string Timer;
    public  int DeathCount = 0;
    public  int EnemiesKilled = 0;
    public int Score = 0;
    public static StatsTracker instance;

    private void Awake() {
        if (instance == null)
            {
                DontDestroyOnLoad(transform.gameObject);
                instance = this;
            }
            else
            {
                DestroyImmediate(gameObject);
            }
    }

    public void SpeedrunModeActivated(){
        SpeedrunMode = true;
    }
}
