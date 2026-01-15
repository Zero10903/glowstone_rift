using Health;
using UnityEngine;

namespace Testing
{
    public class TestTakeDamage : MonoBehaviour
    {
        [SerializeField] private float damageAmount;

        private void OnTriggerEnter2D(Collider2D other)
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
        
            damageable?.TakeDamage(damageAmount);
        }
    }
}
