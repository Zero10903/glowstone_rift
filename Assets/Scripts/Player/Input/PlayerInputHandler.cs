using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Input
{
    public class PlayerInputHandler : MonoBehaviour
    {
        public event Action OnJump;
        public event Action OnMeleeAttack;
        public bool IsJumpPressed { get; private set; }
        public Vector2 MoveInput { get; private set; }

        public void OnMoveInput(InputAction.CallbackContext context)
        {
            MoveInput = context.ReadValue<Vector2>();
        }

        public void OnJumpInput(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                IsJumpPressed = true;
            }
            if (context.canceled)
            {
                IsJumpPressed = false;
            }
            if (context.performed)
            {
                OnJump?.Invoke();
            }
        }

        public void OnMeleeAttackInput(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                Debug.Log("Attack input");
                OnMeleeAttack?.Invoke();
            }
        }
    }
}