using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StatsTracker : MonoBehaviour
{

    public bool SpeedrunMode;
    public string Timer;
    public  int DeathCount;
    public  int EnemiesKilled = 0;
    public int Score = 0;
    public static StatsTracker instance;
    private TimerUI _timer;
    public bool CanStartTheTimer = true;

    private void Awake() {
        if (instance == null)
            {
                DontDestroyOnLoad(transform.gameObject);
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
    }

    private void Start() {
        _timer = GetComponent<TimerUI>();  
    }

    public void SpeedrunModeActivation(Toggle speedrun){
        if(speedrun.isOn == true){
            SpeedrunMode = true;
        }else{
            SpeedrunMode = false;
        }
    }


    public void StartTheTimer(){
        if(_timer != null)
        _timer.BeginTimer();
    }

}
