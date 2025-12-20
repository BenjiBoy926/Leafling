using UnityEngine;
using NaughtyAttributes;

public class LeaflingState_DashAim : LeaflingState
{
    [SerializeField]
    private float _gravityScale = 0.1f;
    [SerializeField]
    private float _animationTransitionScale = 0.25f;
    [ShowNonSerializedField]
    private Vector2 _aim;

    protected override void OnEnable()
    {
        base.OnEnable();
        Target.SetGravityScale(_gravityScale);
        Target.SetVerticalVelocity(0);
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        Target.ResetGravityScale();
        Target.ResetSpriteRotation();
    }
    protected override void OnDashTargetTouched(DashTarget target)
    {
        base.OnDashTargetTouched(target);
        target.Tickle(Target.DashTargeter);
    }
    protected override void Update()
    {
        base.Update();
        _aim = LeaflingStateTool_Dash.ClampDashAim(Target, Target.DashAim);
        LeaflingStateTool_Dash.TransitionDashPerch(Target, _animationTransitionScale, _aim);
        if (!Target.IsAimingDash)
        {
            Target.SendSignal(new LeaflingSignal_Dash(_aim, true));
        }
    }
}
