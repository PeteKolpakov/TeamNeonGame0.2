using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float _enemySpeed;
    
    public float _stopingDistance;
    
    public float _retreatdistance;

    public float _startTimeShots;

    private float _shootingDelay;

    [SerializeField]
    private Transform _player;

    [SerializeField]
    private GameObject _bulletPrefab;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;

        _shootingDelay = _startTimeShots;
    }
    private void Update()
    {
        CalculateDistance(); //can put the moveTowards method on a Couroutine, to have it not calculate it each frame, want some feedback.Or with NavhMesh, also not using physics, so much questions 

        if(_shootingDelay <= 0)
        {
            Instantiate(_bulletPrefab, transform.position, transform.rotation);
            _shootingDelay = _startTimeShots;
        }
        else
        {
            _shootingDelay -= Time.deltaTime;
        }
    }

    private void CalculateDistance()
    {
        if(Vector2.Distance(transform.position, _player.position) > _stopingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, _player.position, _enemySpeed * Time.deltaTime);

        }else if(Vector2.Distance(transform.position, _player.position) < _stopingDistance && Vector2.Distance(transform.position, _player.position) > _retreatdistance){
            transform.position = this.transform.position;
        }else if(Vector2.Distance(transform.position, _player.position) < _retreatdistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, _player.position, -_enemySpeed * Time.deltaTime);
        }
    }
}
