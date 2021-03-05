using System.Collections;
using UnityEngine;
using TMPro;

public class TimerUI : MonoBehaviour
{
    public TextMeshProUGUI TimerText;
    public TextMeshProUGUI MilisecondsText;
    public GameObject TimerSpace;

    private StatsTracker _stats;
    private float _elapsedTime;

    private void Awake() {
        _stats = GetComponent<StatsTracker>();
    }
    public void BeginTimer(){
        if(_stats.SpeedrunMode == true){
            _elapsedTime = 0f;
             TimerSpace.SetActive(true);
            StartCoroutine(UpdateTimer());
        }
    }
    
    public void StopAndResetTimer(){
        StopCoroutine(UpdateTimer());
        TimerSpace.SetActive(false);
        _elapsedTime = 0;
        _stats.SpeedrunMode = false;
    }

    private IEnumerator UpdateTimer(){
        while(true){
            _elapsedTime += Time.deltaTime;
            float oldTime = _elapsedTime;
            float milliseconds = (Mathf.Floor(_elapsedTime * 100) % 100); // calculate the milliseconds for the timer

            int seconds = (int)(_elapsedTime % 60); // return the remainder of the seconds divide by 60 as an int
            _elapsedTime /= 60; // divide current time by 60 to get minutes
            int minutes = (int)(_elapsedTime % 60); //return the remainder of the minutes divide by 60 as an int
            
            TimerText.text = string.Format("{0}:{1}.", minutes.ToString("00"), seconds.ToString("00"));
            MilisecondsText.text = string.Format("{0}", milliseconds.ToString("00"));
            _elapsedTime = oldTime;

            yield return null;
        }
    }
}
