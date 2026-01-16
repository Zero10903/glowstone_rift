using System.Collections;
using UnityEngine;

namespace Health
{
    public class HealthDamageFeedback : MonoBehaviour
    {
        [Header("Damage Feedback Settings")]
        [SerializeField] private Color color = Color.white;
        [SerializeField] private float flashDuration = 0.1f;
        
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
            StartCoroutine(ApplyFlash());
        }

        private IEnumerator ApplyFlash()
        {
            _spriteRenderer.color = color;
            yield return new WaitForSeconds(flashDuration);
            _spriteRenderer.color = _defaultColor;
        }
    }
}
