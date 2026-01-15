using Player.Input;
using UnityEngine;

namespace Player.Movement
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float movementSpeed = 5f;
        private IMovementBlocker[] _movementBlockers;
        public float MoveDirection { get; private set; }
        private bool _isOnMovement;
        public bool IsOnMovement => _isOnMovement;
        
        private Rigidbody2D _rb;
        private PlayerInputHandler _input;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _input = GetComponent<PlayerInputHandler>();
            
            _movementBlockers = GetComponents<IMovementBlocker>();
            
            _isOnMovement = false;
        }

        private void FixedUpdate()
        {
            if (IsMovementBlocked())
                return;
            
            float x = _input.MoveInput.x;
            MoveDirection = x;
            _rb.linearVelocityX = x * movementSpeed;

            _isOnMovement = _input.MoveInput.x != 0;
        }

        private bool IsMovementBlocked()
        {
            foreach (var movementBlocker in _movementBlockers)
            {
                if (movementBlocker.IsMovementBlocked)
                {
                    return true;
                }
            }
            return false;
        }
    }
}