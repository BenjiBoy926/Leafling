using UnityEngine;

public class LeaflingState_Jump : LeaflingState
{
    private float JumpProgress => TimeSinceStateStart / _maxDuration;

    [SerializeField]
    private SpriteAnimation _animation;
    [SerializeField]
    private float _maxSpeed;
    [SerializeField]
    private float _maxDuration;
    [SerializeField]
    private AnimationCurve _speedCurve;
    [SerializeField]
    private DirectionalPhysicsControl _airControl;

    protected override void OnEnable()
    {
        base.OnEnable();
        Target.SetAnimation(_animation);
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
        Target.SetVerticalVelocity(GetJumpSpeed());
        if (ShouldTransitionOutOfJump())
        {
            Target.SendSignal(new LeaflingSignal_FreeFall(FreeFallEntry.Backflip));
        }
    }
    private bool ShouldTransitionOutOfJump()
    {
        return IsAirborn() && (IsJumpingInputReleased() || IsJumpingTimeExhausted());
    }
    private bool IsAirborn()
    {
        return !Target.IsTouching(CardinalDirection.Down);
    }
    private bool IsJumpingInputReleased()
    {
        return !Target.IsJumping;
    }
    private bool IsJumpingTimeExhausted()
    {
        return TimeSinceStateStart >= _maxDuration;
    }
    private float GetJumpSpeed()
    {
        return _speedCurve.Evaluate(JumpProgress) * _maxSpeed;
    }
}
