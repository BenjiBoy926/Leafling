using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Abstract
{
    public class HeroStateMachine : MonoBehaviour
    {
        private HeroState _currentState;

        public void SetState(HeroState state)
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