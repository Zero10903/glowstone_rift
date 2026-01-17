using UnityEngine;
using Player.Input;
using Utils;

namespace Player.Attack
{
    public class PlayerMeleeAttack : MonoBehaviour
    {
        private PlayerInputHandler _playerInputHandler;

        [Header("Melee Attack Settings")]
        [SerializeField] private float attackCooldown = 1f;
        [SerializeField] private float attackDuration = 0.2f;
        [SerializeField] private float baseDamage = 10f;
        public float BaseDamage => baseDamage;
        
        [Header("Melee Attack Hitbox")]
        [SerializeField] private GameObject meleeHitboxRight;
        [SerializeField] private GameObject meleeHitboxUp;
        [SerializeField] private GameObject meleeHitboxLeft;
        [SerializeField] private GameObject meleeHitboxDown;

        private Vector2 _lastMoveInput = Vector2.right;
        private Vector2 _saveLastMoveInput = Vector2.right;

        private Cooldown _cooldown;
        
        void Awake()
        {
            _playerInputHandler = GetComponent<PlayerInputHandler>();
            
            _cooldown = new Cooldown(attackCooldown);
        }

        private void OnEnable()
        {
            _playerInputHandler.OnMeleeAttack += TryAttack;
        }

        private void OnDisable()
        {
            _playerInputHandler.OnMeleeAttack -= TryAttack;
        }

        private void Update()
        {
            GetDirection();
        }

        private void TryAttack()
        {
            if (!_cooldown.IsReady) return;
            
            _cooldown.Use();
            GetDirection();
            
            HandleMeleeHitbox();
            
            // Deactivate after attack
            Invoke(nameof(DisableMeleeHitboxes), attackDuration);
        }

        private void GetDirection()
        {
            if (_lastMoveInput != Vector2.zero)
                _saveLastMoveInput = _lastMoveInput;
                
            _lastMoveInput = _playerInputHandler.MoveInput;
            float x = _playerInputHandler.MoveInput.x;
            float y = _playerInputHandler.MoveInput.y;
            
            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                _lastMoveInput = x > 0 ? Vector2.right : Vector2.left;
            }
            else if (Mathf.Abs(y) > Mathf.Abs(x))
            {
                _lastMoveInput = y > 0 ? Vector2.up : Vector2.down;
            }
            else
            {
                _lastMoveInput = _saveLastMoveInput;
            }
        }

        private void HandleMeleeHitbox()
        {
            if (_lastMoveInput == Vector2.right)
                meleeHitboxRight.SetActive(true);
            else if (_lastMoveInput == Vector2.up)
                meleeHitboxUp.SetActive(true);
            else if (_lastMoveInput == Vector2.left)
                meleeHitboxLeft.SetActive(true);
            else if (_lastMoveInput == Vector2.down)
                meleeHitboxDown.SetActive(true);
            
        }
        
        private void DisableMeleeHitboxes()
        {
            meleeHitboxRight.SetActive(false);
            meleeHitboxUp.SetActive(false);
            meleeHitboxLeft.SetActive(false);
            meleeHitboxDown.SetActive(false);
        }
        
    }
}
