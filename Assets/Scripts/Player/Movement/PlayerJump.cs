using System;
using Player.Input;
using UnityEngine;
using Utils;

namespace Player.Movement
{
    public class PlayerJump : MonoBehaviour
    {
        [SerializeField] private float jumpForce = 5;
        
        private PlayerInputHandler _playerInputHandler;
        private Rigidbody2D _rb;
        private GroundChecker _groundChecker;

        private void Awake()
        {
            _playerInputHandler = GetComponent<PlayerInputHandler>();
            _rb = GetComponent<Rigidbody2D>();
            _groundChecker = GetComponent<GroundChecker>();
        }

        void OnEnable()
        {
            _playerInputHandler.OnJump += TryJump;
        }
        
        void OnDisable()
        {
            _playerInputHandler.OnJump -= TryJump;
        }

        private void TryJump()
        {
            if (_groundChecker.IsGrounded)
            {
                Debug.Log("Jump");
                _rb.linearVelocityY = jumpForce;
            }
        }
    }
}
