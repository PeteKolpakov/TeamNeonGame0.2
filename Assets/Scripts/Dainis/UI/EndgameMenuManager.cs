using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Assets.Scripts.GameManager;

public class EndgameMenuManager : MonoBehaviour
{
    public TMP_Text finalTime;
    public TMP_Text deaths;
    public TMP_Text kills;

    public GameObject speedrunText;
    public GameObject normalrunText;

    private void Start() {
        StatsTracker stats = GameObject.FindGameObjectWithTag("GameManager").GetComponent<StatsTracker>();

        finalTime.text = stats.Timer;
        deaths.text = stats.DeathCount.ToString();
        kills.text = stats.EnemiesKilled.ToString();


        if(stats.SpeedrunMode == true){
            speedrunText.SetActive(true);
            normalrunText.SetActive(false);
        } else if(stats.SpeedrunMode == false){
            normalrunText.SetActive(true);
            speedrunText.SetActive(false);
        }
    }
}
