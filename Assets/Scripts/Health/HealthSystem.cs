using System;
using Interfaces;
using UnityEngine;

namespace Health
{
    public class HealthSystem : MonoBehaviour, IDamageable, IHealable,  IKillable
    {
        [Header("Health Settings")]
        [SerializeField] protected float maxHealth;
        private float _currentHealth;
        
        // Only read variables
        public float CurrentHealth => _currentHealth;
        public float MaxHealth => maxHealth;

        public event Action<float> OnHeal;
        public event Action<float> OnDamageTaken;
        public event Action<float> OnDeath;
        public event Action<float, float> OnHealthChanged;

        protected virtual void Awake()
        {
            _currentHealth = maxHealth;
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
        
        public virtual void TakeDamage(float damageAmount)
        {
            // Check if damage is negative
            if (damageAmount < 0)
            {
                Debug.LogError("Damage cannot be negative");
                return;
            }
            
            // Take damage
            _currentHealth =  Mathf.Clamp(_currentHealth - damageAmount, 0, maxHealth);
            
            // Invoke events
            OnDamageTaken?.Invoke(_currentHealth);
            OnHealthChanged?.Invoke(_currentHealth, maxHealth);

            // Check if the object has died
            if (_currentHealth <= 0)
            {
                OnDeath?.Invoke(_currentHealth);
                Die();
            }
        }

        public virtual void Die()
        {
            Destroy(gameObject);
        }
    }
}
