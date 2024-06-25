using System;

namespace Leafling
{
    public abstract class LeaflingSignal
    {
        public abstract Type StateType { get; }
        public virtual void PrepareNextState(LeaflingStateMachine machine) { }
    }
}