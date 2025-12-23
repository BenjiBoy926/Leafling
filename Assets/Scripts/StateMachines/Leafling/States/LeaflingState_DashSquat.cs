using UnityEngine;
using NaughtyAttributes;

public class LeaflingState_DashSquat : LeaflingState
{
    [SerializeField, ReadOnly]
    private Vector2 _aim;
    [SerializeField, ReadOnly]
    private bool _dashOnRicochet;

    public void SetAim(Vector2 aim)
    {
        _aim = aim;
    }
    public void SetDashOnRicochet(bool dashOnRicochet)
    {
        _dashOnRicochet = dashOnRicochet;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        LeaflingStateTool_Dash.ShowDashPerch(Target, _aim);
        Target.TakeControlOfReticle();
        Target.ShowAim(_aim);
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
        Target.SendSignal(new LeaflingSignal_Dash(_aim, _dashOnRicochet));
    }
}
