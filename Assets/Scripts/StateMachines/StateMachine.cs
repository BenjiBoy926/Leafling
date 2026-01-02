using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine<TTarget> : MonoBehaviour, IStateMachine where TTarget : MonoBehaviour
{
    public string StateListPropertyPath => nameof(_stateList);
    public abstract Type BaseStateType { get; }
    public MonoBehaviour Behaviour => this;

    [SerializeField]
    private State<TTarget> _currentState;
    [SerializeField]
    private List<State<TTarget>> _stateList;
    private Dictionary<Type, State<TTarget>> _stateIndex;

    private void Awake()
    {
        _stateIndex = BuildStateIndex(_stateList);
        DisableAllStates();
    }
    private void Start()
    {
        SetState(_currentState);
    }
    private static Dictionary<Type, State<TTarget>> BuildStateIndex(List<State<TTarget>> allStates)
    {
        Dictionary<Type, State<TTarget>> result = new();
        foreach (State<TTarget> state in allStates)
        {
            result[state.GetType()] = state;
        }
        return result;
    }
    private void DisableAllStates()
    {
        for (int i = 0; i < _stateList.Count; i++)
        {
            _stateList[i].enabled = false;
        }
    }

    public void SendSignal(ISignal<TTarget> signal)
    {
        signal.PrepareNextState(this);
        SetState(signal.StateType);
    }
    public void SetState(Type stateType)
    {
        SetState(GetState(stateType));
    }
    public void SetState(State<TTarget> state)
    {
        if (_currentState != null)
        {
            _currentState.enabled = false;
        }
        _currentState = state;
        if (_currentState != null)
        {
            _currentState.enabled = true;
        }
    }

    public TState GetState<TState>() where TState : State<TTarget>
    {
        return GetState(typeof(TState)) as TState;
    }
    private State<TTarget> GetState(Type stateType)
    {
        return _stateIndex[stateType];
    }
}
