using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Input
{
    public class PlayerInputHandler : MonoBehaviour
    {
        public Vector2 MoveInput { get; private set; }

        public void OnMoveInput(InputAction.CallbackContext context)
        {
            MoveInput = context.ReadValue<Vector2>();
        }
    }
}