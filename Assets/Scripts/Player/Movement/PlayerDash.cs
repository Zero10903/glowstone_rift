using UnityEngine;
using System.Collections;
using Player.Gravity;
using Player.Input;

namespace Player.Movement
{
    public class PlayerDash : MonoBehaviour, IMovementBlocker, IGravityBlocker
    {
        [SerializeField] private float dashForce = 20f;
        [SerializeField] private float dashDuration = 0.2f;
        private float _originalGravity;
        private bool _canDash;
        private bool _isDashing;
        public bool IsMovementBlocked => _isDashing;
        public bool IsGravityBlocked => _isDashing;
        
        private PlayerInputHandler _inputHandler;
        private Rigidbody2D _rb;
        private PlayerMovement _playerMovement;
        
        private void Awake()
        {
            _inputHandler = GetComponent<PlayerInputHandler>();
            _rb = GetComponent<Rigidbody2D>();
            _playerMovement = GetComponent<PlayerMovement>();
            
            _canDash = true;
            _isDashing = false;
        }

        void OnEnable()
        {
            _inputHandler.OnDash += HandleDash;
        }

        void OnDisable()
        {
            _inputHandler.OnDash -= HandleDash;
        }

        private void HandleDash()
        {
            if (!_canDash || _isDashing || !_playerMovement.IsOnMovement)
                return;

            StartCoroutine(DashRoutine());
        }

        private IEnumerator DashRoutine()
        {
            _canDash = false;
            _isDashing = true;
            
            _originalGravity = _rb.gravityScale;
            _rb.gravityScale = 0f;
            _rb.linearVelocityY = 0f;
            _rb.linearVelocityX = dashForce * _playerMovement.MoveDirection;

            yield return new WaitForSeconds(dashDuration);

            _rb.gravityScale = _originalGravity;
            _rb.linearVelocityY = _rb.linearVelocityY;
            _rb.linearVelocityX = 0f;

            _isDashing = false;
            _canDash = true;
        }
    }
}
