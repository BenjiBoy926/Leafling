using System;

namespace Leafling
{
    public interface ILeaflingSignal
    {
        Type StateType { get; }
        void PrepareNextState(LeaflingStateMachine machine);
    }
}