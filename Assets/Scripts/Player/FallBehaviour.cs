using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallBehaviour: MonoBehaviour
{
    [SerializeField]
    private float _radius;

    private RaycastHit2D[] _groundedHits = new RaycastHit2D[2];
    private PlayerMovement _playerMovement;
    private SpriteRenderer _sprite;
    private Vector2 _exitPoint;
    private float _scale;
    private bool _isFalling;

    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _sprite = GetComponent<SpriteRenderer>();
        _isFalling = false;
    }
    private void FixedUpdate()
    {
        if (!IsGrounded())
        {
            if(!_playerMovement.IsDashing()&& !_isFalling)
                Fall();
        }
        else
        {
            _exitPoint = _groundedHits[1].collider.transform.position;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
    private bool IsGrounded()
    {
        ContactFilter2D contactFilter2D = new ContactFilter2D();
        contactFilter2D.useTriggers = true;
        
        int count = Physics2D.CircleCast(transform.position, _radius, Vector3.zero, contactFilter2D, _groundedHits);
        return count > 1;
    }

    public void Fall()
    {
        _scale = 0.5f;
        _sprite.sortingOrder = 0;
        _isFalling = true;
        
        _playerMovement.DisableMovement();

        StartCoroutine(DelayedRespawn(1f));
    }
    
    
    private void Respawn()
    {
        Debug.Log("Respawned");
        _playerMovement.EnableMovement();
        transform.position = _exitPoint;
        transform.localScale = new Vector3(1f, 1f, 1f);
        _sprite.sortingOrder = 2;
        _isFalling = false;
    }
    private IEnumerator DelayedRespawn(float duration)
    {
        while(duration > 0)
        {
            yield return null;
            duration -= Time.deltaTime;
            if (_scale > 0f)
            {
                _scale -= Time.deltaTime;
                transform.localScale = new Vector3(_scale, _scale, _scale);
            }
        }
        Respawn();
    }
}