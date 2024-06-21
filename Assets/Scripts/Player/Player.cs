using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Leafling
{
    public class Player : MonoBehaviour, DefaultActions.IDefaultMapActions
    {
        [SerializeField]
        private LeaflingInputs _inputs;
        [SerializeField]
        private Camera _mouseConversionCamera;
        private DefaultActions _actions;

        private void Reset()
        {
            _inputs = GetComponent<LeaflingInputs>();
        }
        private void Awake()
        {
            _actions = new DefaultActions();
            _actions.DefaultMap.SetCallbacks(this);
            _mouseConversionCamera = Camera.main;
        }
        private void OnEnable()
        {
            _actions.Enable();
        }
        private void OnDisable()
        {
            _actions.Disable();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            float axis = context.ReadValue<float>();
            int direction = 0;
            if (axis < -0.1f)
            {
                direction = -1;
            }
            if (axis > 0.1f)
            {
                direction = 1;
            }
            _inputs.SetHorizontalDirection(direction);
        }
        public void OnJump(InputAction.CallbackContext context)
        {
            _inputs.SetIsJumping(context.ReadValueAsButton());
        }
        public void OnDashTarget(InputAction.CallbackContext context)
        {
            Vector2 screenPoint = context.ReadValue<Vector2>();
            Vector2 worldPoint = GetWorldPointFromScreenPoint(screenPoint);
            _inputs.SetDashTarget(worldPoint);
        }
        public void OnDash(InputAction.CallbackContext context)
        {
            _inputs.SetIsAimingDash(context.ReadValueAsButton());
        }
        public void OnCrouch(InputAction.CallbackContext context)
        {
            _inputs.SetIsCrouching(context.ReadValueAsButton());
        }

        private Vector2 GetWorldPointFromScreenPoint(Vector2 screenPoint)
        {
            return _mouseConversionCamera.ScreenToWorldPoint(screenPoint);
        }
    }
}