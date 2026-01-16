using System.Collections;
using Player.Movement;
using UnityEngine;

namespace Health
{
    public class HealthDamageFeedback : MonoBehaviour, IMovementBlocker
    {
        [Header("Damage Flash Settings")]
        [SerializeField] private Color color = Color.white;
        [SerializeField] private float flashDuration = 0.1f;
        private Color _defaultColor;

        [Header("Hit Stop Settings")] [SerializeField]
        private bool isHitStopActive = true;
        [SerializeField] private float hitStopDuration = 0.03f;
        [SerializeField] private float hitStopTimeScale = 0f;
        
        [Header("Knockback Settings")]
        [SerializeField] private float knockbackForce = 10f;
        [SerializeField] private ForceMode2D knockbackForceMode = ForceMode2D.Impulse;
        [SerializeField] private float knockbackDuration = 0.1f;
        private bool _isKnockbackActive = false;
        
        public bool IsMovementBlocked => _isKnockbackActive;
        
        private SpriteRenderer _spriteRenderer;
        private Rigidbody2D _rb;
        private HealthSystem _healthSystem;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _rb = GetComponent<Rigidbody2D>();
            _healthSystem = GetComponent<HealthSystem>();
            
            _defaultColor = _spriteRenderer.color;
        }

        private void OnEnable()
        {
            _healthSystem.OnDamageTaken += HandleFeedback;
        }

        private void OnDisable()
        {
            _healthSystem.OnDamageTaken -= HandleFeedback;
        }

        private void HandleFeedback(DamageData damageData)
        {
            StartCoroutine(FlashRoutine());
            
            if(isHitStopActive)
                StartCoroutine(HitStopRoutine());

            if (!_isKnockbackActive)
                StartCoroutine(KnockbackRoutine(damageData));
        }

        private IEnumerator FlashRoutine()
        {
            _spriteRenderer.color = color;
            yield return new WaitForSeconds(flashDuration);
            _spriteRenderer.color = _defaultColor;
        }

        private IEnumerator HitStopRoutine()
        {
            float originalTimeScale = Time.timeScale;
            Time.timeScale = hitStopTimeScale;
            yield return new WaitForSecondsRealtime(hitStopDuration);
            Time.timeScale = originalTimeScale;
        }

        private IEnumerator KnockbackRoutine(DamageData damageData)
        {
            _isKnockbackActive = true;
            _rb.linearVelocity = Vector2.zero;
            Vector2 direction = damageData.damageDirection.normalized;
            _rb.AddForce(direction * knockbackForce, knockbackForceMode);
            yield return new WaitForSeconds(knockbackDuration);
            _isKnockbackActive = false;
        }
    }
}
