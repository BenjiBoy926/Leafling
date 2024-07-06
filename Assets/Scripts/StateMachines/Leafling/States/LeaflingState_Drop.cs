using UnityEngine;

public class LeaflingState_Drop : LeaflingState
{
    [SerializeField]
    private SpriteAnimation _animation;
    [SerializeField]
    private float _animationTransitionScale = 0.1f;
    [SerializeField]
    private float _dropSpeed = 30;
    [SerializeField]
    private float _cancelSpeed = 10;
    [SerializeField]
    private DirectionalAirControl _airControl;

    protected override void OnEnable()
    {
        base.OnEnable();
        Target.SetTransition(new(_animation, _animationTransitionScale, Target.CurrentFlipX));
    }
    protected override void OnStartedJumping()
    {
        base.OnStartedJumping();
        if (HasEnteredActionFrame)
        {
            Target.SetVerticalVelocity(_cancelSpeed);
        }
        Target.SendSignal(new LeaflingSignal_FreeFall(FreeFallEntry.Backflip));
    }
    protected override void OnDashTargetTouched(DashTarget target)
    {
        base.OnDashTargetTouched(target);
        Target.SendSignal(new LeaflingSignal_DashSpin(target));
    }

    protected override void Update()
    {
        base.Update();
        Target.ApplyAirControl(_airControl);
        if (HasEnteredActionFrame)
        {
            Target.SetVerticalVelocity(-_dropSpeed);
        }
        else
        {
            Target.SetVerticalVelocity(0);
        }
        if (Target.IsTouching(CardinalDirection.Down))
        {
            Target.SendSignal(new LeaflingSignal_Landing(new LeaflingSignal<LeaflingState_DropJump>()));
        }
    }
}