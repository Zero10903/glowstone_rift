using Player.Input;
using UnityEngine;

namespace Player.Gravity
{
    public class PlayerGravityController : MonoBehaviour
    {
        [Header("Custom Gravity")]
        [SerializeField] private float fallGravityMultiplier = 2f;
        [SerializeField] private float lowJumpGravityMultiplier = 2f;

        private float _defaultGravityScale;
        private IGravityBlocker[]  _gravityBlockers;
        
        private Rigidbody2D _rb;
        private PlayerInputHandler _input;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _input = GetComponent<PlayerInputHandler>();
            
            _gravityBlockers = GetComponents<IGravityBlocker>();
            
            _defaultGravityScale = _rb.gravityScale;
        }

        private void Update()
        {
            if (IsGravityBlocked())
                return;
            
            ApplyCustomGravity();
        }
        
        private void ApplyCustomGravity()
        {
            // When fall
            if (_rb.linearVelocityY < 0)
            {
                _rb.gravityScale = _defaultGravityScale * fallGravityMultiplier;
            }
            // When fall but jump btn is not pressed (short jump)
            else if (_rb.linearVelocityY > 0 && !_input.IsJumpPressed)
            {
                _rb.gravityScale = _defaultGravityScale * lowJumpGravityMultiplier;
            }
            else
            {
                // Normal gravity
                _rb.gravityScale = _defaultGravityScale;
            }
        }

        private bool IsGravityBlocked()
        {
            foreach (var gravityBlocker in _gravityBlockers)
            {
                if (gravityBlocker.IsGravityBlocked)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
