using UnityEngine;

public class LeaflingState_Flutter : LeaflingState
{
    [SerializeField]
    private SpriteAnimation _animation;
    [SerializeField]
    private float _animationTransitionScale = 0.5f;
    [SerializeField]
    private float _speed = 5;
    [SerializeField]
    private DirectionalAirControl _restingAirControl;
    [SerializeField]
    private DirectionalAirControl _actionAirControl;

    protected override void OnEnable()
    {
        base.OnEnable();
        Target.SetTransition(new(_animation, _animationTransitionScale, Target.CurrentFlipX));
    }
    protected override void OnStartedAimingDash()
    {
        base.OnStartedAimingDash();
        if (Target.IsAbleToDash)
        {
            Target.SendSignal(new LeaflingSignal<LeaflingState_DashAim>());
        }
    }
    protected override void OnAnimationFinished()
    {
        base.OnAnimationFinished();
        if (Target.IsAnimating(_animation))
        {
            Target.SendSignal(new LeaflingSignal_FreeFall(FreeFallEntry.Normal));
        }
    }
    protected override void OnAnimationEnteredActionFrame()
    {
        base.OnAnimationEnteredActionFrame();
        Target.SetVerticalVelocity(_speed);
    }
    protected override void OnDashTargetTouched(DashTarget target)
    {
        base.OnDashTargetTouched(target);
        target.Tickle();
    }

    protected override void Update()
    {
        base.Update();
        ApplyAirControl();
        if (Target.IsTouching(CardinalDirection.Down))
        {
            Target.SendSignal(new LeaflingSignal_Landing(new LeaflingSignal<LeaflingState_Jump>()));
        }
        LeaflingStateTool_WallJump.CheckTransitionToWallSlide(Target);
    }
    private void ApplyAirControl()
    {
        if (Target.IsCurrentFrameActionFrame)
        {
            Target.ApplyAirControl(_actionAirControl);
        }
        else
        {
            Target.ApplyAirControl(_restingAirControl);
        }
    }
}
