using Health;
using UnityEngine;

namespace Player.Health
{
    public class PlayerHealth : HealthSystem
    {
        public override void Die()
        {
            base.Die();
            Debug.Log("Player has dead.");
        }
    }
}
