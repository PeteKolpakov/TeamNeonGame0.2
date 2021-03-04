using UnityEngine;

public class StatsTracker : MonoBehaviour
{

    public bool SpeedrunMode;
    public string Timer;
    public int DeathCount = 0;
    public int EnemiesKilled = 0;
    public int Score = 0;
    public static StatsTracker Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(transform.gameObject);
            Instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    public void SpeedrunModeActivated()
    {
        SpeedrunMode = true;
    }
}
