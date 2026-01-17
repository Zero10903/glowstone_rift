using System.Collections;
using Health;
using UnityEngine;

namespace Testing
{
    public class DummyHealth : HealthSystem
    {
        [Header("Dummy Settings")]
        [SerializeField] private float respawnTime = 3f;
        public override void Die()
        {
            StartCoroutine(Respawn());
        }

        private IEnumerator Respawn()
        {
            yield return new WaitForSeconds(respawnTime);
            Heal(maxHealth);
        }
    }
}
