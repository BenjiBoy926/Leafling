using UnityEngine;
using UnityEngine.Serialization;

public class LeaflingState_DropJump : LeaflingState
{
    [SerializeField]
    private SpriteAnimation _animation;
    [SerializeField, FormerlySerializedAs("_speed")]
    private float _maxSpeed;
    [SerializeField]
    private AnimationCurve _speedCurve;
    [SerializeField]
    private DirectionalPhysicsControl _airControl;

    protected override void OnEnable()
    {
        base.OnEnable();
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
    }
    protected override void Update()
    {
        base.Update();
        Target.ApplyPhysicsControl(_airControl);

        float verticalVelocity = _speedCurve.Evaluate(Target.CurrentAnimationProgress) * _maxSpeed;
        Target.SetVerticalVelocity(verticalVelocity);
    }
}
