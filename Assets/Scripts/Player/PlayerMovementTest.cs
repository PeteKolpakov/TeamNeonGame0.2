using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementTest : MonoBehaviour
{
    [SerializeField]
    private LayerMask _dashLayerMask;

    [SerializeField]
    private float _movementSpeed = 250f;

    [SerializeField]
    private float _dashLenght;

    private float _dashTimer;

    private bool _wannaDash;

    private bool _isDashing;

    private Vector2 _direction;

    public Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        Move();

        if (Input.GetKeyDown(KeyCode.Space))
            _wannaDash = true;
    }
    private void MoveWithGetAxis()
    {
        _wannaDash = false;
        _isDashing = false;
        _direction.x = Input.GetAxis("Horizontal");
        _direction.y = Input.GetAxis("Vertical");
    }
    private void Move()
    {
        float moveX = 0f;
        float moveY = 0f;
        if (Input.GetKey(KeyCode.W))
        {
            moveY += +1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveY -= 1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveX +=1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveX -= 1f;
        }

        _direction = new Vector2(moveX, moveY).normalized;
    }
    private void FixedUpdate()
    {
        rb.velocity = _direction * _movementSpeed * Time.deltaTime;         //for Move() (with 250f as movement speed works)

        Dash();

        //rb.MovePosition(rb.position + _direction * _movementSpeed * Time.deltaTime); FOR MoveWithGetAxis() (with about 10f)
    }
    private void Dash()
    {
        
        if (_wannaDash)
        {
            Vector2 dashPosition = ((Vector2)transform.position + _direction * _dashLenght);

            RaycastHit2D raycastHit2d = Physics2D.Raycast(transform.position, _direction, _dashLenght,_dashLayerMask);
            
            if (raycastHit2d.collider != null)
            {
                Debug.DrawLine(rb.position, raycastHit2d.point, Color.cyan, 2f);    //saw this on Dainis' prototype c:
                dashPosition = raycastHit2d.point;
            }

            //Debug.DrawLine(rb.position, raycastHit2d.point, Color.cyan, 2f);
            rb.MovePosition(dashPosition);

            _wannaDash = false;
        }
    }
}
