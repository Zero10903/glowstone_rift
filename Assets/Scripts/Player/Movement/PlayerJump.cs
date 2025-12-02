using System;
using Player.Input;
using UnityEngine;
using Utils;

namespace Player.Movement
{
    public class PlayerJump : MonoBehaviour
    {
        [Header("Jump Values")]
        [SerializeField] private float jumpForce = 5f;

        [Header("Coyote Time")]
        [SerializeField] private float coyoteTime = 0.1f;
        private float _coyoteTimer;

        [Header("Jump Buffer")]
        [SerializeField] private float jumpBufferTime = 0.1f;
        private float _jumpBufferTimer;
        
        [Header("Custom Gravity")]
        [SerializeField] private float fallGravityMultiplier = 2f;
        [SerializeField] private float lowJumpGravityMultiplier = 2f;

        private float _defaultGravityScale;
        
        private PlayerInputHandler _playerInputHandler;
        private Rigidbody2D _rb;
        private GroundChecker _groundChecker;

        private void Awake()
        {
            _playerInputHandler = GetComponent<PlayerInputHandler>();
            _rb = GetComponent<Rigidbody2D>();
            _groundChecker = GetComponent<GroundChecker>();
            
            _defaultGravityScale = _rb.gravityScale;
        }

        void OnEnable()
        {
            _playerInputHandler.OnJump += BufferJump;
        }
        
        void OnDisable()
        {
            _playerInputHandler.OnJump -= BufferJump;
        }
        
        private void Update()
        {
            HandleCoyoteTime();
            HandleJumpBuffer();
            ApplyCustomGravity();
        }
        
        // ================
        // CUSTOM GRAVITY
        // ================
        private void ApplyCustomGravity()
        {
            // When fall
            if (_rb.linearVelocityY < 0)
            {
                _rb.gravityScale = _defaultGravityScale * fallGravityMultiplier;
            }
            // When fall but jump btn is not pressed (short jump)
            else if (_rb.linearVelocityY > 0 && !_playerInputHandler.IsJumpPressed)
            {
                _rb.gravityScale = _defaultGravityScale * lowJumpGravityMultiplier;
            }
            else
            {
                // Normal gravity
                _rb.gravityScale = _defaultGravityScale;
            }
        }
        
        // ================
        // COYOTE TIME
        // ================
        private void HandleCoyoteTime()
        {
            if (_groundChecker.IsGrounded)
                _coyoteTimer = coyoteTime;  
            else
                _coyoteTimer -= Time.deltaTime;
        }
        
        // ================
        // JUMP BUFFER
        // ================
        private void BufferJump()
        {
            _jumpBufferTimer = jumpBufferTime;
        }

        private void HandleJumpBuffer()
        {
            if (_jumpBufferTimer > 0)
            {
                TryJump();
                _jumpBufferTimer -= Time.deltaTime;
            }
        }

        // ================
        // JUMP
        // ================
        private void TryJump()
        {
            if (_coyoteTimer <= 0) return;
            
            // jump
            _rb.linearVelocityY = jumpForce;
            
            // Reset timers after jump
            _coyoteTimer = 0;
            _jumpBufferTimer = 0;
        }
    }
}
