using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Assets.Scripts.GameManager;
using UnityEngine.SceneManagement;


public class TimerUI : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI milisecondsText;
    public GameObject timerSpace;

    private StatsTracker _stats;
    private float _elapsedTime;

    private void Awake() {
        _stats = GetComponent<StatsTracker>();
        
    }
    private void Start() {
    }

    public void BeginTimer(){
        if(_stats.SpeedrunMode == true){
            _elapsedTime = 0f;
             timerSpace.SetActive(true);
            StartCoroutine(UpdateTimer());
        }
    }

    private IEnumerator UpdateTimer(){
        while(true){
            _elapsedTime += Time.deltaTime;
            float oldTime = _elapsedTime;
            float milliseconds = (Mathf.Floor(_elapsedTime * 100) % 100); // calculate the milliseconds for the timer

            int seconds = (int)(_elapsedTime % 60); // return the remainder of the seconds divide by 60 as an int
            _elapsedTime /= 60; // divide current time by 60 to get minutes
            int minutes = (int)(_elapsedTime % 60); //return the remainder of the minutes divide by 60 as an int
            
            timerText.text = string.Format("{0}:{1}.", minutes.ToString("00"), seconds.ToString("00"));
            milisecondsText.text = string.Format("{0}", milliseconds.ToString("00"));
            _elapsedTime = oldTime;

            yield return null;
        }
    }
}
