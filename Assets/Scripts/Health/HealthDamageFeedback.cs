using System.Collections;
using UnityEngine;

namespace Health
{
    public class HealthDamageFeedback : MonoBehaviour
    {
        [Header("Flashback Feedback Settings")]
        [SerializeField] private Color color = Color.white;
        [SerializeField] private float flashDuration = 0.1f;

        [Header("Hit Stop Feedback Settings")] [SerializeField]
        private bool isHitStopActive = true;
        [SerializeField] private float hitStopDuration = 0.03f;
        [SerializeField] private float hitStopTimeScale = 0f;
        
        private Color _defaultColor;
        private float _damageAmount;
        
        private SpriteRenderer _spriteRenderer;
        private HealthSystem _healthSystem;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
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

        private void HandleFeedback()
        {
            StartCoroutine(FlashRoutine());
            StartCoroutine(HitStopRoutine());
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
    }
}
