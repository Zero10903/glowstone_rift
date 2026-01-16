using System;
using System.Collections;
using UnityEngine;

namespace Health
{
    public class HealthSystem : MonoBehaviour, IDamageable, IHealable,  IKillable
    {
        [Header("Health Settings")]
        [SerializeField] protected float maxHealth = 100f;
        [SerializeField] protected float invulnerabilityTime = 0.2f;
        
        private float _currentHealth;
        private bool _isInvulnerable;
        
        // Only read variables
        public float CurrentHealth => _currentHealth;
        public float MaxHealth => maxHealth;

        public event Action<float> OnHeal;
        public event Action<DamageData> OnDamageTaken;
        public event Action<float> OnDeath;
        public event Action<float, float> OnHealthChanged;

        protected virtual void Awake()
        {
            _currentHealth = maxHealth;
            _isInvulnerable = false;
        }
        
        public virtual void Heal(float healAmount)
        {
            // Check if heal is negative
            if (healAmount < 0)
            {
                Debug.LogError("Heal amount cannot be negative");
                return;
            }
            
            // Check if the object has max health
            if (Mathf.Approximately(_currentHealth, maxHealth)) return;
            
            // Heal
            _currentHealth = Mathf.Clamp(_currentHealth + healAmount, 0, maxHealth);
            
            // Invoke events
            OnHeal?.Invoke(_currentHealth);
            OnHealthChanged?.Invoke(_currentHealth, maxHealth);
        }
        
        public virtual void TakeDamage(float damageAmount, Vector2 direction, GameObject source)
        {
            if (_isInvulnerable) return;
            
            // Check if damage is negative
            if (damageAmount < 0)
            {
                Debug.LogError("Damage cannot be negative");
                return;
            }
            
            // Take damage
            _currentHealth =  Mathf.Clamp(_currentHealth - damageAmount, 0, maxHealth);

            DamageData damageData = new DamageData()
            {
                damageDirection = direction,
                damageSource = source,
            };
            
            // Invoke events
            OnDamageTaken?.Invoke(damageData);
            OnHealthChanged?.Invoke(_currentHealth, maxHealth);

            // Check if the object has died
            if (_currentHealth <= 0)
            {
                OnDeath?.Invoke(_currentHealth);
                Die();
            }
            else
            {
                StartCoroutine(InvulnerabilityRoutine());
            }
        }

        public virtual void Die()
        {
            Destroy(gameObject);
        }

        protected virtual IEnumerator InvulnerabilityRoutine()
        {
            _isInvulnerable =  true;
            yield return new WaitForSeconds(invulnerabilityTime);
            _isInvulnerable = false;
        }
    }
}
