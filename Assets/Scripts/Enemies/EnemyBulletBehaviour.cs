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
       _bulletTarget = EnemyBehaviour.Direction;
    }
    private void Update()
    {
        Destroy(gameObject, 6f);
    }
    private void FixedUpdate()
    {
        _rb.velocity = _bulletTarget.normalized * _speed;
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
