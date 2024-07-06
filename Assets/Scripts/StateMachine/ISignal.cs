using System;
using UnityEngine;

namespace Leafling
{
    public interface ISignal<TTarget> where TTarget : MonoBehaviour
    {
        Type StateType { get; }
        void PrepareNextState(StateMachine<TTarget> machine);
    }
}