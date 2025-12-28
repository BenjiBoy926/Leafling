using UnityEngine;
using NaughtyAttributes;

public class LeaflingState_DashAim : LeaflingState
{
    [SerializeField]
    private float _gravityScale = 0.1f;
    [SerializeField]
    private float _animationTransitionScale = 0.25f;

    protected override void OnEnable()
    {
        base.OnEnable();
        Target.SetGravityScale(_gravityScale);
        Target.SetVerticalVelocity(0);
        Target.SetDashReticleHighlight();
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        Target.ResetGravityScale();
        Target.ResetSpriteRotation();
        Target.ClearDashReticleHighlight();
    }
    protected override void OnDashTargetTouched(DashTarget target)
    {
        base.OnDashTargetTouched(target);
        target.Tickle(Target.DashTargeter);
    }
    protected override void Update()
    {
        base.Update();
        LeaflingStateTool_Dash.TransitionDashPerch(Target, _animationTransitionScale, Target.DashAim);
        if (!Target.IsAimingDash)
        {
            Target.SendSignal(new LeaflingSignal_Dash(Target.DashAim, true));
        }
    }
}
