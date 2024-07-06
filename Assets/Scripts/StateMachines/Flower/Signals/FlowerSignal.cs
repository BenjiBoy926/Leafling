using System;
using UnityEngine;

public class FlowerSignal<TState> : ISignal<Flower> where TState : FlowerState
{
    public Type StateType => typeof(TState);   

    public void PrepareNextState(StateMachine<Flower> machine)
    {

    }
}
