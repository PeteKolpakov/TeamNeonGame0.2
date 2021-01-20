using System;
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

    [SerializeField]
    private float _dashDelay = .5f;      // dash delay utils
    private float _timeSinceDash = 0f;

    private float _scale;

    private float _dashTimer;

    private bool _canMove;

    private bool _wannaDash;            // dash delay utils
    private bool _canDash;

    private bool _isDashing;

    private bool _isFalling;

    private Vector2 _direction;

    public Rigidbody2D rb;

    public SpriteRenderer sprite;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        _canMove = true;

        _timeSinceDash = 0;
}
    private void Update()
    {
        MoveInput();

        if (Input.GetKeyDown(KeyCode.Space) && _canDash == true)
            _wannaDash = true;

        _timeSinceDash += Time.deltaTime; // update dash timer
        DashCheck();
    }

    private void DashCheck() // checks lastTimeDashed vs. dashDelay to see if dashing is possible 
    {
        if (_timeSinceDash >= _dashDelay)
        {
            _canDash = true;
        }
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
        if (Input.GetKey(KeyCode.W) && !_isFalling)
        {
            moveY += +1f;
        }
        if (Input.GetKey(KeyCode.S) &&!_isFalling)
        {
            moveY -= 1f;
        }
        if (Input.GetKey(KeyCode.D) &&!_isFalling)
        {
            moveX +=1f;
        }
        if (Input.GetKey(KeyCode.A) && !_isFalling)
        {
            moveX -= 1f;
        }

        _direction = new Vector2(moveX, moveY).normalized;
    }
    private void Dash()
    {
        if (_wannaDash && _canDash)
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

            _timeSinceDash = 0; // reset dash timer
            _canDash = false;   // reset ability to dash

            _wannaDash = false; //Bool to be used if the dash movement doesn't want to be used constantly
        }
        _isDashing = false; //If _isDashing is false, and the player finds itself on top of a Pit, he falls.
    }
    private void Falling()
    {
        //this.rb.gravityScale = +_fallGravity;
        _scale = 0.5f;
        sprite.sortingOrder = 0;
        _isFalling = true;
        DecreasePlayerScale();
        
        if((Vector2)transform.position != Vector2.zero)
        {
            Invoke(nameof(Respawn), 3f);           
        }
    }
    public void Respawn()
    {
        Debug.Log("Respawned");
        transform.position = Vector2.zero;
        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        sprite.sortingOrder = 2;
        this.rb.gravityScale = 0;
        _isFalling = false;
        CancelInvoke();
    }
    public void DecreasePlayerScale()
    {
        //Can also be done trough coroutines or animations
        if(_scale > 0f)
        {
            _scale -= 0.1f;
            transform.localScale = new Vector3(_scale, _scale, _scale);
        }
    }
}
