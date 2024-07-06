using System;
using UnityEngine;

public interface ISignal<TTarget> where TTarget : MonoBehaviour
{
    Type StateType { get; }
    void PrepareNextState(StateMachine<TTarget> machine);
}
