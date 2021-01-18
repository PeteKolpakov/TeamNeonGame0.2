using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float _speed;

    private Transform _player;
    private Vector2 _target;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;

        _target = new Vector2(_player.position.x, _player.position.y); // for like guided missiles or something like that and using MoveTowards, not Translate
    }
    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, _target, _speed * Time.deltaTime);

        if(transform.position.x == _target.x && transform.position.y == _target.y)
        {
            Destroy(gameObject);
        }
    }

}
