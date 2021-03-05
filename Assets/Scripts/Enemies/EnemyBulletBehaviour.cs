using Assets.Scripts.Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.GameManager;


public class EnemyBulletBehaviour : MonoBehaviour
{
    public float _speed;

    private Transform _player;

    private Vector2 _bulletTarget;

    public Rigidbody2D _rb;

    private void Start()
    {
       // _bulletTarget = EnemyBehaviour.Direction;
       _bulletTarget = PlayerTracker.Instance.Player.transform.position;
    }
    private void Update()
    {
        //transform.position = Vector2.MoveTowards(transform.position, _bulletTarget, _speed * Time.deltaTime);

        if (transform.position.x == _bulletTarget.x && transform.position.y == _bulletTarget.y)
        {
            Destroy(gameObject);
        }
    }
    private void FixedUpdate()
    {
        _rb.MovePosition(Vector2.MoveTowards(_rb.position, _bulletTarget, Time.deltaTime * _speed));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Entity health))
        {
            health.TakeDamage(20, DamageType.Bullet);
        }
        Destroy(gameObject);
    }
}
