using UnityEngine;

public class LeaflingState_DropJump : LeaflingState
{
    [SerializeField]
    private SpriteAnimation _animation;
    [SerializeField]
    private float _speed = 30;
    [SerializeField]
    private DirectionalAirControl _airControl;

    protected override void OnEnable()
    {
        base.OnEnable();
        Target.SetVerticalVelocity(_speed);
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
        target.Tickle();
    }
    protected override void Update()
    {
        base.Update();
        Target.ApplyAirControl(_airControl);
    }
}
