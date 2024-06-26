using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Leafling
{
    public class LeaflingStateMachine : MonoBehaviour
    {
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
        private void RefreshEnabledStates()
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

#if UNITY_EDITOR
        [Button]
        private void AddAllStates()
        {
            _stateList = LoadAllStates();
            RefreshEnabledStates();
        }
        [Button]
        private void RemoveAllStates()
        {
            for (int i = 0; i < _stateList.Count; i++)
            {
                DestroyImmediate(_stateList[i]);
            }
            _stateList.Clear();
        }
        private List<LeaflingState> LoadAllStates()
        {
            return new()
            {
                gameObject.AddComponent<LeaflingState_Crouch>(),
                gameObject.AddComponent<LeaflingState_CrouchJump>(),
                gameObject.AddComponent<LeaflingState_Dash>(),
                gameObject.AddComponent<LeaflingState_DashAim>(),
                gameObject.AddComponent<LeaflingState_DashCancel>(),
                gameObject.AddComponent<LeaflingState_DashSquat>(),
                gameObject.AddComponent<LeaflingState_Drop>(),
                gameObject.AddComponent<LeaflingState_DropJump>(),
                gameObject.AddComponent<LeaflingState_Flutter>(),
                gameObject.AddComponent<LeaflingState_FreeFall>(),
                gameObject.AddComponent<LeaflingState_Jump>(),
                gameObject.AddComponent<LeaflingState_JumpSquat>(),
                gameObject.AddComponent<LeaflingState_Landing>(),
                gameObject.AddComponent<LeaflingState_LongJump>(),
                gameObject.AddComponent<LeaflingState_Slide>(),
                gameObject.AddComponent<LeaflingState_Standing>(),
                gameObject.AddComponent<LeaflingState_WallJump>(),
                gameObject.AddComponent<LeaflingState_WallSlide>()
            };
        }
#endif
    }
}