using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AbstractHumanoidToy
{
    public class Player : MonoBehaviour, DefaultActions.IDefaultMapActions
    {
        [SerializeField]
        private HeroMovement _movement;
        private DefaultActions _actions;

        private void Reset()
        {
            _movement = GetComponent<HeroMovement>();
        }
        private void Awake()
        {
            _actions = new DefaultActions();
            _actions.DefaultMap.SetCallbacks(this);
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
            _movement.SetCurrentDirection(direction);
        }
    }
}