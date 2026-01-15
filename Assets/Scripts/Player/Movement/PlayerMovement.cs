using Player.Input;
using UnityEngine;

namespace Player.Movement
{
    public class PlayerMovement : MonoBehaviour, IMovementState
    {
        [SerializeField] private float movementSpeed = 5f;
        
        private IMovementBlocker[] _movementBlockers;
        
        private bool _isOnMovement;
        public bool IsOnMovement => _isOnMovement;
        
        private float _movementDirection;
        public float MovementDirection => _movementDirection;
        
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
            
            _movementDirection = _input.MoveInput.x;
            _rb.linearVelocityX = _movementDirection * movementSpeed;

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