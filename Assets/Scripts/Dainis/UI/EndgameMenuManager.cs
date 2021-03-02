using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndgameMenuManager : MonoBehaviour
{
    public TMP_Text finalTime;
    public TMP_Text deaths;
    public TMP_Text kills;

    private void Start() {
        StatsTracker stats = GameObject.FindGameObjectWithTag("GameManager").GetComponent<StatsTracker>();

        finalTime.text = stats.Timer;
        deaths.text = stats.DeathCount.ToString();
        kills.text = stats.EnemiesKilled.ToString();
    }
}
