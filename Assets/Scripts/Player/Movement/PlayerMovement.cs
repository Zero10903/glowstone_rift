using Player.Input;
using UnityEngine;

namespace Player.Movement
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float movementSpeed = 5f;

        private Rigidbody2D _rb;
        private PlayerInputHandler _input;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _input = GetComponent<PlayerInputHandler>();
        }

        private void FixedUpdate()
        {
            float x = _input.MoveInput.x;
            _rb.linearVelocityX = x * movementSpeed;
        }
    }
}