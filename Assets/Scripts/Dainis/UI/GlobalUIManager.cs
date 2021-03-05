using Assets.Scripts.GameManager;
using TMPro;
using UnityEngine;

class GlobalUIManager : MonoBehaviour
{
    private PlayerStatManager _playerStatManager;
    public HealthBar PlayerHealthbar;
    private StatsTracker _stats;

    [SerializeField]
    private Entity _player;
    public TMP_Text ScoreDisplay;
    public TMP_Text HealthDisplay;



    private void Start()
    {
        _player = PlayerTracker.Instance.Player.GetComponent<Entity>();
        _playerStatManager = PlayerTracker.Instance.Player.GetComponent<PlayerStatManager>();
        _stats = GameObject.FindGameObjectWithTag("GameManager").GetComponent<StatsTracker>();

        PlayerHealthbar.SetMaxHealth(_player.health);
        
        // This is a switch, that controls the start of the timer.
        // We start it at the very first time we load the level
        // and change the bool on StatsTracker. Because StatsTracker
        // doesn't get destroyed between the scenes, bool never changes
        // anymore, so the timer doesn't get reset with the new
        // scene load. I don't know how to do it otherwise...
        if(_stats.CanStartTheTimer == true){
            _stats.StartTheTimer();
            _stats.CanStartTheTimer = false;
        }

    }

    private void Update()
    {
        PlayerHealthbar.SetHealth(_player.health);

        ScoreDisplay.text = _stats.Score.ToString();
        HealthDisplay.text = _player.health.ToString() + " \\ " + _player.maxHealth.ToString();
    }
}

