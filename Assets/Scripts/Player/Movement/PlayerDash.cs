using System;
using UnityEngine;
using System.Collections;
using Player.Gravity;
using Player.Input;
using Utils;

namespace Player.Movement
{
    public class PlayerDash : MonoBehaviour, IMovementBlocker, IGravityBlocker
    {
        [Header("Dash Settings")]
        [SerializeField] private float dashForce = 20f;
        [SerializeField] private float dashDuration = 0.2f;
        [SerializeField] private float dashCooldown = 0.6f;
        
        private float _originalGravity;
        private bool _isDashing;
        public bool IsMovementBlocked => _isDashing;
        public bool IsGravityBlocked => _isDashing;
        
        private IMovementState _movementState;
        private Cooldown _cooldown;
        
        private PlayerInputHandler _inputHandler;
        private Rigidbody2D _rb;
        
        private void Awake()
        {
            _inputHandler = GetComponent<PlayerInputHandler>();
            _rb = GetComponent<Rigidbody2D>();
            
            _movementState = GetComponent<IMovementState>();

             _cooldown = new Cooldown(dashCooldown);
             
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
            if (!_cooldown.IsReady || _isDashing || !_movementState.IsOnMovement)
                return;

            _cooldown.Use();
            StartCoroutine(DashRoutine());
        }

        private IEnumerator DashRoutine()
        {
            _isDashing = true;
            
            _originalGravity = _rb.gravityScale;
            _rb.gravityScale = 0f;
            _rb.linearVelocityY = 0f;
            _rb.linearVelocityX = dashForce * _movementState.MovementDirection;

            yield return new WaitForSeconds(dashDuration);

            _rb.gravityScale = _originalGravity;
            _rb.linearVelocityY = _rb.linearVelocityY;
            _rb.linearVelocityX = 0f;

            _isDashing = false;
        }
    }
}
