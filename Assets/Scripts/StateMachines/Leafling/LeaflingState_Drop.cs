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
    private DirectionalPhysicsControl _airControl;

    protected override void OnEnable()
    {
        base.OnEnable();
        Target.SetTransition(new(_animation, _animationTransitionScale, Target.CurrentFlipX));
    }
    protected override void OnStartedJumping()
    {
        base.OnStartedJumping();
        if (Target.IsCurrentFrameActionFrame)
        {
            Target.SetVerticalVelocity(_cancelSpeed);
        }
        LeaflingState_FreeFall.Enter(Target, FreeFallEntry.Backflip);
    }
    protected override void OnDashTargetTouched(DashTarget target)
    {
        base.OnDashTargetTouched(target);
        if (Target.IsCurrentFrameActionFrame)
        {
            LeaflingState_DashSpin.Enter(Target, target);
        }
    }

    protected override void Update()
    {
        base.Update();
        Target.ApplyPhysicsControl(_airControl);
        if (Target.IsCurrentFrameActionFrame)
        {
            Target.SetVerticalVelocity(-_dropSpeed);
        }
        else
        {
            Target.SetVerticalVelocity(0);
        }
        if (Target.IsTouching(CardinalDirection.Down))
        {
            Target.SetState<LeaflingState_DropLanding>();
        }
    }
}