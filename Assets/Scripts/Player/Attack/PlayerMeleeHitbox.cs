using Health;
using UnityEngine;

namespace Player.Attack
{
    public class PlayerMeleeHitbox : MonoBehaviour
    {
        private PlayerMeleeAttack _playerMeleeAttack;

        private void Awake()
        {
            _playerMeleeAttack = GetComponentInParent<PlayerMeleeAttack>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            if (damageable == null) return;
            
            Vector3 direction = (other.transform.position - transform.position).normalized;
            
            damageable.TakeDamage(_playerMeleeAttack.BaseDamage, direction, gameObject);
        }
    }
}
