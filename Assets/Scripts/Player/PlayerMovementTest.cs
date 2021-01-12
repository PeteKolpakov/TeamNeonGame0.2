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

    [SerializeField]
    private float _fallGravity;

    private float _dashTimer;

    private bool _canMove;

    private bool _wannaDash;

    private bool _isDashing;

    private Vector2 _direction;

    public Rigidbody2D rb;

    public SpriteRenderer sprite;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        _canMove = true;
}
    private void Update()
    {
        MoveInput();

        if (Input.GetKeyDown(KeyCode.Space))
            _wannaDash = true;
    }
    private void FixedUpdate()
    {
        if (_canMove)
        {
            rb.velocity = _direction * _movementSpeed * Time.fixedDeltaTime;         //for Move() (with 400f as movement speed works)
        }
        

        Dash();

        //rb.MovePosition(rb.position + _direction * _movementSpeed * Time.fixedDeltaTime); FOR MoveWithGetAxis() (with about 10f)
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Over a Pit");
        if(!_isDashing)
            Falling();
    }
    private void MoveWithGetAxis()
    {
        _wannaDash = false;
        _isDashing = false;
        _direction.x = Input.GetAxis("Horizontal");
        _direction.y = Input.GetAxis("Vertical");
    }
    private void MoveInput()
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
    private void Dash()
    {
        if (_wannaDash)
        {
            _isDashing = true; //Collider being ignored while dashing
            Vector2 dashPosition = ((Vector2)transform.position + _direction * _dashLenght);

            RaycastHit2D raycastHit2d = Physics2D.Raycast(transform.position, _direction, _dashLenght,_dashLayerMask);
            
            if (raycastHit2d.collider != null)
            {
                Debug.DrawLine(rb.position, raycastHit2d.point, Color.cyan, 2f);
                dashPosition = raycastHit2d.point;
            }

            //Debug.DrawLine(rb.position, raycastHit2d.point, Color.cyan, 2f);
            rb.MovePosition(dashPosition);

            _wannaDash = false; //Bool to be used if the dash movement doesn't want to be used constantly
        }
        _isDashing = false; //If _isDashing is false, and the player finds itself on top of a Pit, he falls.
    }
    private void Falling()
    {
        this.rb.gravityScale = +_fallGravity;
        sprite.sortingOrder = 0;
        //Trying to make its position (0,0), but it does it more then once before you're able to move freely again. Basically Respawnning more than twice. Help :c
        if((Vector2)transform.position != Vector2.zero)
        {
            Invoke(nameof(Respwan), 3f);
        }
    }
    public void Respwan()
    {
        Debug.Log("Respawned");
        transform.position = Vector2.zero;
        sprite.sortingOrder = 2;
        this.rb.gravityScale = 0;
    }
}
