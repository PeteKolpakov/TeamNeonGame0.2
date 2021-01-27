using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    [Range(1,10)]
    private float _dashLenght;
    [SerializeField]
    [Range(0.1f,0.5f)]
    private float _dashDuration;
    [SerializeField]
    private LayerMask _dashLayerMask;
    [SerializeField]
    private float _dashCooldown = 0.5f;
    [SerializeField]
    private float _movementSpeed = 400f;

    private float _timeSinceDash = 0f;
    private bool _wannaDash;
    private bool _canMove = true;
    private Vector2 _moveDirection;
    private Rigidbody2D rigidBody;

    private void Start()
    {
        _timeSinceDash = 0;
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        MoveInput();

        if(Input.GetKeyDown(KeyCode.Space) && CanDash())
            _wannaDash = true;

        _timeSinceDash += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (_canMove && !IsDashing())
        {
            rigidBody.velocity = _moveDirection * _movementSpeed * Time.fixedDeltaTime;
            if (_wannaDash && CanDash())
                DashStart();
        }
    }

    public bool IsDashing()
    {
        return _timeSinceDash <= _dashDuration;
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
            moveX += 1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveX -= 1f;
        }

        _moveDirection = new Vector2(moveX, moveY).normalized;
    }

    private void DashStart()
    {       
        Vector2 dashPosition = ((Vector2)transform.position + _moveDirection * _dashLenght);
        /**RaycastHit2D raycastHit2d = Physics2D.Raycast(transform.position, _direction, _dashLenght, _dashLayerMask);
           
        if (raycastHit2d.collider != null)
        {
            Debug.DrawLine(rb.position, raycastHit2d.point, Color.cyan, 2f);
            dashPosition = raycastHit2d.point;
        }**/
        Vector2 dashVelocity = (dashPosition - (Vector2)transform.position) / _dashDuration;
        rigidBody.velocity = dashVelocity;
        _timeSinceDash = 0;        
        _wannaDash = false;
    }

    private bool CanDash()
    {
        return _timeSinceDash >= _dashCooldown;     
    }

    public void EnableMovement()
    {
        _canMove = true;
    }

    public void DisableMovement()
    {
        _canMove = false;
        rigidBody.velocity = Vector2.zero;
    }
}
