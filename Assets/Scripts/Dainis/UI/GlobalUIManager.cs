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
        PlayerShoot playerShoot = _player.GetComponent<PlayerShoot>();
        _stats = GameObject.FindGameObjectWithTag("GameManager").GetComponent<StatsTracker>();

        PlayerHealthbar.SetMaxHealth(_player.Health);
    }

    private void Update()
    {
        PlayerShoot playerShootScript = _player.GetComponent<PlayerShoot>();

        PlayerHealthbar.SetHealth(_player.Health);

        ScoreDisplay.text = _stats.Score.ToString();
        HealthDisplay.text = _player.Health.ToString() + " \\ " + _player.MaxHealth.ToString();
    }
}

