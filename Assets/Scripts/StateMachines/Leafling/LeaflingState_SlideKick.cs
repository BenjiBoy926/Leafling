using UnityEngine;

public class LeaflingState_SlideKick : LeaflingState
{
    [SerializeField]
    private SpriteAnimation _animation;
    [SerializeField]
    private float _startupVelocity = 25;

    protected override void OnEnable()
    {
        base.OnEnable();
        float x = Target.FacingDirection * _startupVelocity;
        Vector2 velocity = new(x, _startupVelocity);
        Target.SetVelocity(velocity);
        Target.SetAnimation(_animation);
    }
    protected override void OnAnimationFinished()
    {
        base.OnAnimationFinished();
        LeaflingState_FreeFall.Enter(Target, FreeFallEntry.Backflip);
    }
    protected override void OnDashTargetTouched(DashTarget target)
    {
        base.OnDashTargetTouched(target);
        target.Tickle(Target.DashTargeter);
        // Actually should attack
    }
}
