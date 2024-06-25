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
        private LeaflingState _currentState;
        private List<LeaflingState> _stateList;
        private Dictionary<Type, LeaflingState> _stateIndex;

        private void Awake()
        {
            _stateList = LoadAllStates();
            _stateIndex = BuildStateIndex(_stateList);
        }
        private List<LeaflingState> LoadAllStates()
        {
            return new()
            {
                new LeaflingState_Crouch(_leafling),
                new LeaflingState_CrouchJump(_leafling),
                new LeaflingState_Dash(_leafling, Vector2.zero, true),
                new LeaflingState_DashAim(_leafling),
                new LeaflingState_DashCancel(_leafling, Vector2.zero),
                new LeaflingState_DashSquat(_leafling, Vector2.zero, true),
                new LeaflingState_Drop(_leafling),
                new LeaflingState_DropJump(_leafling),
                new LeaflingState_Flutter(_leafling),
                new LeaflingState_FreeFall(_leafling, FreeFallEntry.Normal),
                new LeaflingState_Jump(_leafling),
                new LeaflingState_JumpSquat(_leafling),
                new LeaflingState_Landing(_leafling),
                new LeaflingState_LongJump(_leafling),
                new LeaflingState_Slide(_leafling),
                new LeaflingState_Standing(_leafling),
                new LeaflingState_WallJump(_leafling, CardinalDirection.Up),
                new LeaflingState_WallSlide(_leafling, CardinalDirection.Up)
            };
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

        public void SendSignal(LeaflingSignal signal)
        {
            signal.PrepareNextState(this);
            SetState(signal.StateType);
        }
        private void SetState(Type stateType)
        {
            SetState(GetState(stateType));
        }
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

        public LeaflingState GetState(Type stateType)
        {
            return _stateIndex[stateType];
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