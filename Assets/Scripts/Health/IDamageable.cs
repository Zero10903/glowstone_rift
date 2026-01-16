using UnityEngine;

namespace Health
{
    public interface IDamageable
    {
        void TakeDamage(float damage, Vector2 direction, GameObject  source);
    }
}