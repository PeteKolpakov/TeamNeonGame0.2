using Assets.Scripts.EntityClass;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class FallBehaviour: MonoBehaviour
    {
        [SerializeField]
        private float _radius;
        private Transform _currentGround;
        private PlayerMovement _playerMovement;
        private PlayerBase _playerHealth;
        private SpriteRenderer _sprite;
        private Vector2 _exitPoint;
        private Rigidbody2D _rigidBody;
        private float _scale;
        private float _lastRespawnTime;
        public bool _isFalling;
        [SerializeField]
        private LayerMask _groundCheckMask;

        private void Start()
        {
            _playerMovement = GetComponent<PlayerMovement>();
            _playerHealth = GetComponent<PlayerBase>();
            _sprite = GetComponent<SpriteRenderer>();
            _rigidBody = GetComponent<Rigidbody2D>();
            _isFalling = false;
        }
        private void FixedUpdate()
        {
            if (!IsGrounded())
            {
                if(!_playerMovement.IsDashing()&& !_isFalling && !IsRespawnInvincible())
                    Fall();
            }
            else
            {
                if (!_playerMovement.IsDashing())
                {
                    _exitPoint = _currentGround.position;
                }
               
            }
        }
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
        private bool IsGrounded()
        {
            RaycastHit2D[] groundedHits = new RaycastHit2D[2];
            ContactFilter2D contactFilter2D = new ContactFilter2D();
            contactFilter2D.useTriggers = true;
            contactFilter2D.useLayerMask = true;
            contactFilter2D.layerMask = _groundCheckMask;
            
        
            int count = Physics2D.CircleCast(transform.position, _radius, Vector3.zero, contactFilter2D, groundedHits);
            foreach (var item in groundedHits)
            {
                if(item.collider != null)
                {
                    if(item.collider.gameObject != gameObject)
                    {
                        _currentGround = item.collider.transform;
                        return true;
                    }
                }
            }
            _currentGround = null;
            return false;
        }

        public void Fall()
        {
            _scale = 0.5f;
            _sprite.sortingOrder = 0;
            _isFalling = true;

            //Remove Health
            _playerHealth.TakeDamage(10, DamageType.Fall);

            _playerMovement.DisableMovement();
            StartCoroutine(DelayedRespawn(1f));

        }
        public bool IsRespawnInvincible()
        {
            return Time.time - _lastRespawnTime < 0.25;
        }
    
        private void Respawn()
        {
            _lastRespawnTime = Time.time;
            
            _playerMovement.EnableMovement();
            transform.position = _exitPoint;
            _rigidBody.position = _exitPoint;
            transform.localScale = new Vector3(1f, 1f, 1f);
            _sprite.sortingOrder = 2;
            _isFalling = false;
            _rigidBody.velocity = new Vector2(0,0);
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
}