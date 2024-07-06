using UnityEngine;

public class LeaflingSignal_DashSquat : LeaflingSignal<LeaflingState_DashSquat>
{
    private Vector2 _aim;
    private bool _dashOnRicochet;

    public LeaflingSignal_DashSquat(Vector2 aim, bool dashOnRichochet)
    {
        _aim = aim;
        _dashOnRicochet = dashOnRichochet;
    }
    protected override void PrepareNextState(LeaflingState_DashSquat state)
    {
        base.PrepareNextState(state);
        state.SetAim(_aim);
        state.SetDashOnRicochet(_dashOnRicochet);
    }
}
