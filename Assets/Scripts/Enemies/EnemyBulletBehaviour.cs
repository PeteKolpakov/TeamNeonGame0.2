using Assets.Scripts.Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletBehaviour : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _lifeTime = 10f;

    private Vector2 _bulletDirection;
    [SerializeField]
    private Rigidbody2D _rb;

    private void Start() // is there a reason not to put this in awake? (question for Luca)
    {
        _rb = GetComponent<Rigidbody2D>();
        _bulletDirection = EnemyBehaviour.Direction;
    }
    private void Update()
    {
        transform.Translate(Vector3.right*Time.deltaTime*_speed,Space.Self); // TODO Change to Rb2D movement
        Destroy(gameObject, _lifeTime);

    }
    private void FixedUpdate()
    {
        //_rb.MovePosition(_bulletDirection);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Entity health))
        {
            health.TakeDamage(20);
        }
        Destroy(gameObject);
    }

  
}
