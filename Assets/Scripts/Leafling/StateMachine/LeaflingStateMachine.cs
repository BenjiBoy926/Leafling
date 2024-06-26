using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Leafling
{
    public class LeaflingStateMachine : MonoBehaviour
    {
        public const string StateListPropertyPath = nameof(_stateList);

        [SerializeField]
        private Leafling _leafling;
        [SerializeField]
        private List<LeaflingState> _stateList;
        private LeaflingState _currentState;
        private Dictionary<Type, LeaflingState> _stateIndex;

        private void Awake()
        {
            _stateIndex = BuildStateIndex(_stateList);
            RefreshEnabledStates();
        }
        private static Dictionary<Type, LeaflingState> BuildStateIndex(List<LeaflingState> allStates)
        {
            Dictionary<Type, LeaflingState> result = new();
            foreach (LeaflingState state in allStates)
            {
                result[state.GetType()] = state;
            }
            return result;
        }

        public void SendSignal(ILeaflingSignal signal)
        {
            signal.PrepareNextState(this);
            SetState(signal.StateType);
        }
        private void SetState(Type stateType)
        {
            SetState(GetState(stateType));
        }
        private void SetState(LeaflingState state)
        {
            _currentState = state;
            RefreshEnabledStates();
        }
        public void RefreshEnabledStates()
        {
            for (int i = 0; i < _stateList.Count; i++)
            {
                RefreshStateEnabled(_stateList[i]);
            }
        }
        private void RefreshStateEnabled(LeaflingState state)
        {
            state.enabled = state == _currentState;
        }

        public TState GetState<TState>() where TState : LeaflingState
        {
            return GetState(typeof(TState)) as TState;
        }
        public LeaflingState GetState(Type stateType)
        {
            return _stateIndex[stateType];
        }
    }
}