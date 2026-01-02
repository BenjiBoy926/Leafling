using UnityEngine;
using NaughtyAttributes;

public class LeaflingState_DashSquat : LeaflingState
{
    [SerializeField, ReadOnly]
    private Vector2 _aim;
    [SerializeField, ReadOnly]
    private bool _dashOnRicochet;

    public static void Enter(Leafling target, Vector2 aim, bool dashOnRicochet)
    {
        LeaflingState_DashSquat state = target.GetState<LeaflingState_DashSquat>();
        state._aim = aim;
        state._dashOnRicochet = dashOnRicochet;
        target.SetState(state);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        LeaflingStateTool_Dash.ShowDashPerch(Target, _aim);
        Target.TakeControlOfReticle();
        Target.ShowAim(_aim);
        Target.SetVelocity(Vector2.zero);
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        Target.ReleaseControlOfReticle();
    }
    protected override void OnAnimationFinished()
    {
        base.OnAnimationFinished();
        _aim = LeaflingStateTool_Dash.ClampDashAim(Target, _aim);
        LeaflingState_Dash.Enter(Target, _aim, _dashOnRicochet);
    }
}
