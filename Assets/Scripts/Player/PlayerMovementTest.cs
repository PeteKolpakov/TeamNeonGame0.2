using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementTest : MonoBehaviour
{
    [SerializeField]
    private float _movementSpeed = 3f;

    private Vector2 _direction;

    private Vector2 _mousePosition;

    public Rigidbody2D rb;

    private void Update()
    {
        _direction.x = Input.GetAxis("Horizontal");
        _direction.y = Input.GetAxis("Vertical");
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + _direction * _movementSpeed * Time.deltaTime);
    }
}
