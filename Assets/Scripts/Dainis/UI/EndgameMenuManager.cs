using TMPro;
using UnityEngine;

public class EndgameMenuManager : MonoBehaviour
{
    public TMP_Text SpeedrunFinalTime;
    public TMP_Text SpeedrunDeaths;
    public TMP_Text SpeedrunKills;
    public TMP_Text SpeedrunScore;
    public TMP_Text NormalScore;

    public TMP_Text NormalDeaths;
    public TMP_Text NormalKills;

    public GameObject SpeedrunText;
    public GameObject NormalrunText;

    private void Start()
    {
        StatsTracker stats = GameObject.FindGameObjectWithTag("GameManager").GetComponent<StatsTracker>();

        SpeedrunFinalTime.text = stats.Timer;
        SpeedrunDeaths.text = stats.DeathCount.ToString();
        SpeedrunKills.text = stats.EnemiesKilled.ToString();

        NormalDeaths.text = stats.DeathCount.ToString();
        NormalKills.text = stats.EnemiesKilled.ToString();

        SpeedrunScore.text = stats.Score.ToString();
        NormalScore.text = stats.Score.ToString();


        if (stats.SpeedrunMode == true)
        {
            SpeedrunText.SetActive(true);
            NormalrunText.SetActive(false);
        }
        else if (stats.SpeedrunMode == false)
        {
            NormalrunText.SetActive(true);
            SpeedrunText.SetActive(false);
        }
    }

    public void QuitGame(){
        Application.Quit();
    }
}
