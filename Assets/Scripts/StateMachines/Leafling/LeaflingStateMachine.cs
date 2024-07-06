using System;

public class LeaflingStateMachine : StateMachine<Leafling>
{
    public override Type BaseStateType => typeof(LeaflingState);
}
