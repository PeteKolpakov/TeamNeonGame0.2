using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Speed;

    private Transform _player;
    private Vector2 _direction;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;

        _direction = new Vector2(_player.position.x, _player.position.y);
    }
    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, _direction, Speed * Time.deltaTime);

        if (transform.position.x == _direction.x && transform.position.y == _direction.y)
        {
            Destroy(gameObject);
        }
    }

}
