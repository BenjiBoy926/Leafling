using UnityEngine;

public class LeaflingState_SlideKick : LeaflingState
{
    [SerializeField]
    private SpriteAnimation _animation;
    [SerializeField]
    private float _upwardVelocity = 10;

    protected override void OnEnable()
    {
        base.OnEnable();
        Target.SetVerticalVelocity(_upwardVelocity);
        Target.SetAnimation(_animation);
    }
    protected override void OnAnimationFinished()
    {
        base.OnAnimationFinished();
        Target.SendSignal(new LeaflingSignal_FreeFall(FreeFallEntry.Backflip));
    }
    protected override void OnDashTargetTouched(DashTarget target)
    {
        base.OnDashTargetTouched(target);
        target.Tickle(Target.DashTargeter);
        // Actually should attack
    }
}
