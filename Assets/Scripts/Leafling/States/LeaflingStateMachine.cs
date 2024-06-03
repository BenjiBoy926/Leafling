using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Leafling
{
    public class LeaflingStateMachine : MonoBehaviour
    {
        private LeaflingState _currentState;

        public void SetState(LeaflingState state)
        {
            if (_currentState != null)
            {
                _currentState.Exit();
            }
            _currentState = state;
            if (_currentState != null)
            {
                _currentState.Enter();
            }
        }
        private void Update()
        {
            if (_currentState == null)
            {
                return;
            }
            _currentState.Update(Time.deltaTime);
        }
    }
}