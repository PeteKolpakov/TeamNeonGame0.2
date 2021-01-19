using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallScript : MonoBehaviour
{
    public SpriteRenderer sprite;

    private float _scale;

    public bool _canMove;

    public bool _isFalling;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        _canMove = true;
        _isFalling = false;
    }
}
