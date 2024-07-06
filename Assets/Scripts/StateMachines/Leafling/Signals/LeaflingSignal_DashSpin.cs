using System.Collections;
using UnityEngine;

public class LeaflingSignal_DashSpin : LeaflingSignal<LeaflingState_DashSpin>
{
    private DashTarget _target;

    public LeaflingSignal_DashSpin(DashTarget target)
    {
        _target = target;
    }
    protected override void PrepareNextState(LeaflingState_DashSpin state)
    {
        base.PrepareNextState(state);
        state.SetDashTarget(_target);
    }
}
