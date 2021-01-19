using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementTest2WIP : MonoBehaviour
{
    [SerializeField]
    private LayerMask _dashLayerMask;

    [SerializeField]
    private float _movementSpeed = 250f;

    [SerializeField]
    private float _dashLenght;

    private bool _wannaDash;

    private bool _isDashing;

    private Vector2 _direction;

    public Rigidbody2D rb;

    private void Start()
    {
        FallScript fallscript = gameObject.GetComponent<FallScript>();
        fallscript._isFalling = false;
    }
    private void MoveInput()
    {
        float moveX = 0f;
        float moveY = 0f;
        if (Input.GetKey(KeyCode.W)) //&& !_isFalling)
        {
            moveY += +1f;
        }
        if (Input.GetKey(KeyCode.S)) //&& !_isFalling)
        {
            moveY -= 1f;
        }
        if (Input.GetKey(KeyCode.D)) //&& !_isFalling)
        {
            moveX += 1f;
        }
        if (Input.GetKey(KeyCode.A)) //&& !_isFalling)
        {
            moveX -= 1f;
        }

        _direction = new Vector2(moveX, moveY).normalized;
    }
}
