using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallScriptWIP: MonoBehaviour
{
    public SpriteRenderer sprite;

    private float _scale;

    public bool _canMove;

    public static bool _isFalling;

    public Rigidbody2D rb;


    private void Awake()
    {
        PlayerMovementTest2WIP playerMovementTest2WIP = gameObject.GetComponent<PlayerMovementTest2WIP>();
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        _canMove = true;
        _isFalling = false;
    }
   
  
    public void Fall()
    {
        _scale = 0.5f;
        sprite.sortingOrder = 0;
        _isFalling = true;
        DecreasePlayerScale();
        Debug.Log("The dude do be falling tho");

        //if ((Vector2)transform.position != Vector2.zero)
        //Invoke(nameof(Respawn(PlayerMovementTest2WIP._exitPoint), 3f);
        Respawn(PlayerMovementTest2WIP._exitPoint);
        
    }
    public void DecreasePlayerScale()
    {
        //Can also be done trough coroutines or animations
        if (_scale > 0f)
        {
            _scale -= 0.1f;
            transform.localScale = new Vector3(_scale, _scale, _scale);
        }
    }
    public void Respawn(Vector2 position)
    {
        Debug.Log("Respawned");
        transform.position = position;
        transform.localScale = new Vector3(1f, 1f, 1f);
        sprite.sortingOrder = 2;
        this.rb.gravityScale = 0;
        _isFalling = false;
    }
}