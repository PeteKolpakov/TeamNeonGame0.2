using UnityEngine;
using Assets.Scripts.EntityClass;

namespace Assets.Scripts.Player
{
    [RequireComponent(typeof(PlayerAudio))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem _dashAfterImage;
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

        private PlayerAudio _audio;

        private float _timeSinceDash = 0f;
        private bool _wannaDash;
        private bool _canMove = true;
        private Vector2 _moveDirection;
        private Rigidbody2D _rigidBody;

        public GameObject PauseMenu;
        public bool IsPauseMenuOpen;
        private PlayerBase _playerBase;

        private void Start()
        {
            _timeSinceDash = 0;
            _rigidBody = GetComponent<Rigidbody2D>();
            _playerBase = GetComponent<PlayerBase>();
        }

        private void Awake()
        {
            _audio = GetComponent<PlayerAudio>();
        }

        private void Update()
        {
            MoveInput();
            PauseInput();

            if(Input.GetKeyDown(KeyCode.Space) && CanDash())
                _wannaDash = true;

            _timeSinceDash += Time.deltaTime;
        }

        private void FixedUpdate()
        {
            if (_canMove && !IsDashing())
            {
                _rigidBody.velocity = _moveDirection * _movementSpeed * Time.fixedDeltaTime;
                if (_wannaDash && _rigidBody.velocity != new Vector2(0,0) && CanDash())
                    DashStart();
            }
        }

        public bool IsDashing()
        {
            return _timeSinceDash <= _dashDuration;
        }

        private void MoveInput()
        {
            if(IsPauseMenuOpen == false)
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
            else
            {
                return;
            }

        }

        private void PauseInput()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if(IsPauseMenuOpen == true)
                {
                    _audio.PlaySFX(_audio._pauseExitSFX);
                    Time.timeScale = 1;
                    PauseMenu.SetActive(false);
                    IsPauseMenuOpen = false;
                }
                else
                {
                    _audio.PlaySFX(_audio._pauseEnterSFX);
                    Time.timeScale = 0;
                    PauseMenu.SetActive(true);
                    IsPauseMenuOpen = true;
                }
            }
        }
        
        public void ResumeButton()
        {
            Time.timeScale = 1;
            PauseMenu.SetActive(false);
            IsPauseMenuOpen = false;
        }

        private void DashStart()
        {

            _playerBase.canTakeDamage = false;
            _audio.PlaySFX(_audio._dashSFX);
            if(_dashAfterImage != null){
                _dashAfterImage.Play();
            }
            Vector2 dashPosition = ((Vector2)transform.position + _moveDirection * _dashLenght);
            Vector2 dashVelocity = (dashPosition - (Vector2)transform.position) / _dashDuration;
            _rigidBody.velocity = dashVelocity;
            _timeSinceDash = 0;        
            _wannaDash = false;

            _playerBase.canTakeDamage = true;


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
            _rigidBody.velocity = Vector2.zero;
        }
    }
}
