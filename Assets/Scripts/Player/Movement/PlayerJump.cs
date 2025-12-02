using System;
using Player.Input;
using UnityEngine;

namespace Player.Movement
{
    public class PlayerJump : MonoBehaviour
    {
        [SerializeField] private float jumpForce = 5;
        
        private PlayerInputHandler _playerInputHandler;
        private Rigidbody2D _rb;

        private void Awake()
        {
            _playerInputHandler = GetComponent<PlayerInputHandler>();
            _rb = GetComponent<Rigidbody2D>();
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
            Debug.Log("Jump");
            _rb.linearVelocityY = jumpForce;
        }
    }
}
