using System;

namespace Leafling
{
    public class LeaflingSignal_Generic<TState> : LeaflingSignal where TState : LeaflingState
    {
        public override Type StateType => typeof(TState);
    }
}