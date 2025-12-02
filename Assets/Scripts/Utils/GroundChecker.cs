using UnityEngine;

namespace Utils
{
    public class GroundChecker : MonoBehaviour
    {
        [SerializeField] private Transform groundCheckPoint;
        [SerializeField] private Vector2 checkSize = new Vector2(0.5f, 0.1f);
        [SerializeField] private LayerMask groundLayer;
        
        public bool IsGrounded { get; private set; }
        
        private void Update()
        {
            IsGrounded = Physics2D.OverlapBox(
                groundCheckPoint.position,
                checkSize,
                0f,
                groundLayer
            );
        }
        
        private void OnDrawGizmosSelected()
        {
            if (groundCheckPoint == null) return;

            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(groundCheckPoint.position, checkSize);
        }
    }
}
