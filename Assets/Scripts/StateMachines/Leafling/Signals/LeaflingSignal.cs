using System;

namespace Leafling
{
    public class LeaflingSignal<TState> : ILeaflingSignal where TState : LeaflingState
    {
        public Type StateType => typeof(TState);

        public void PrepareNextState(StateMachine<Leafling> machine)
        {
            PrepareNextState(GetState(machine));
        }
        protected TState GetState(StateMachine<Leafling> machine)
        {
            return machine.GetState<TState>();
        }
        protected virtual void PrepareNextState(TState state)
        {

        }
    }
}