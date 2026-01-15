using Health;
using UnityEngine;

namespace Testing
{
    public class TestHeal : MonoBehaviour
    {
        [SerializeField] private float healAmount;

        private void OnTriggerEnter2D(Collider2D other)
        {
            IHealable healable = other.GetComponent<IHealable>();

            healable?.Heal(healAmount);
        }
    }
}
