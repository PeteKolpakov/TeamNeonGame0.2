using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField]
    private float _bulletSpeed;

    private void Start()
    {
        
        Destroy(gameObject, 4f);
    }

    private void Update()
    {
        Move(transform.right);

    }

    private void Move(Vector3 direction)
    {
        transform.Translate(direction.normalized * Time.deltaTime * _bulletSpeed, Space.World);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        PlayerStatManager player = playerObject.GetComponent<PlayerStatManager>();

        if(collision.collider.CompareTag("Enemy"))
        {
            EnemyStatManager enemy = collision.collider.GetComponent<EnemyStatManager>();
            enemy.HurtEnemy(player._damage);
            Destroy(gameObject);
        }
    }
}
