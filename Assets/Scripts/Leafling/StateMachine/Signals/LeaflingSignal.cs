using System;

namespace Leafling
{
    public class LeaflingSignal<TState> : ILeaflingSignal where TState : LeaflingState
    {
        public Type StateType => typeof(TState);

        public void PrepareNextState(LeaflingStateMachine machine)
        {
            PrepareNextState(GetState(machine));
        }
        protected TState GetState(LeaflingStateMachine machine)
        {
            return machine.GetState<TState>();
        }
        protected virtual void PrepareNextState(TState state)
        {

        }
    }
}