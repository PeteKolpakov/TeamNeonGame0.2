using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementTest2WIP : MonoBehaviour
{
    [SerializeField]
    private LayerMask _dashLayerMask;

    [SerializeField]
    private float _movementSpeed = 400f;

    [SerializeField]
    private float _dashLenght;

    [SerializeField]
    private float _fallDelay = 0.5f;

    private float _fallTimer;

    [SerializeField]
    private float _dashDelay = 0.5f;

    private float _timeSinceDash = 0f;

    private bool _canDash;

    private bool _wannaDash;

    private bool _isDashing;

    private Vector2 _direction;

    public static Vector2 _exitPoint;

    public Rigidbody2D rb;

    public FallScriptWIP fallScript;


    private void Awake()
    {
        FallScriptWIP fallScriptWIP = gameObject.GetComponent<FallScriptWIP>();
        Vector2 _playerScale = transform.localScale;
    }

    private void Start()
    {
        _timeSinceDash = 0;
        _exitPoint = transform.position;
        StartCoroutine(FallDelay(_fallDelay));
    }

    private void Update()
    {
        MoveInput();

        if(Input.GetKeyDown(KeyCode.Space) && _canDash == true)
            _wannaDash = true;

        _timeSinceDash += Time.deltaTime; // update dash timer
        DashCheck();
        
        //Debug.Log(_fallTimer);
        if (!FallScriptWIP._isFalling)
        {
            rb.velocity = _direction * _movementSpeed * Time.fixedDeltaTime;
        }
        Dash();
    }

    private void FixedUpdate()
    {
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _exitPoint = collision.transform.position;
        if (!_isDashing && !FallScriptWIP._isFalling)
        {
            Debug.Log("Is gonna fall boys");
            fallScript.Fall();
        }
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

        _direction = new Vector2(moveX, moveY).normalized;
    }
    private void Dash()
   {
        if (_wannaDash && _canDash)
        {
            _isDashing = true; //Collider being ignored while dashing
            Vector2 dashPosition = ((Vector2)transform.position + _direction * _dashLenght);

            Debug.Log("Is Dashing");

            RaycastHit2D raycastHit2d = Physics2D.Raycast(transform.position, _direction, _dashLenght, _dashLayerMask);
           
            if (raycastHit2d.collider != null)
            {
                Debug.DrawLine(rb.position, raycastHit2d.point, Color.cyan, 2f);
                dashPosition = raycastHit2d.point;
            }
            rb.AddForce(dashPosition, ForceMode2D.Impulse);

            _timeSinceDash = 0;
            _canDash = false;

            _wannaDash = false; //Bool to be used if the dash movement doesn't want to be used constantly
        }
        _isDashing = false; //If _isDashing is false, and the player finds itself on top of a Pit, he falls.
        Debug.Log("Not dashing, should Fall");
    }
    private void DashCheck()
    {
        if(_timeSinceDash >= _dashDelay)
        {
            _canDash = true;
        }
    }
    private void FallDelay() //Implement or delete c:
    {
        _fallTimer = Time.deltaTime;
        if (_fallTimer >= _fallDelay)
        {
            FallScriptWIP._isFalling = true;
            _isDashing = false;
            Debug.Log("NOt dashing anymore :c");
            _fallTimer = 0;
        }
    }
    private IEnumerator FallDelay(float recoveryTime)
    {
        while (_isDashing)
        {
            yield return new WaitForSeconds(recoveryTime);
            _isDashing = false;
            
        }
    }
}
