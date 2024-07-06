using UnityEngine;

public class LeaflingState_Slide : LeaflingState
{
    [SerializeField]
    private SpriteAnimation _animation;
    [SerializeField]
    private float _animationTransitionScale = 0.25f;
    [SerializeField]
    private float _maxSpeed = 30;
    [SerializeField]
    private AnimationCurve _speedCurve;

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
            Target.SendSignal(new LeaflingSignal<LeaflingState_LongJump>());
        }
    }
    protected override void OnAnimationStarted()
    {
        base.OnAnimationStarted();
        Target.SetHorizontalVelocity(0);
    }
    protected override void OnAnimationFinished()
    {
        base.OnAnimationFinished();
        if (Target.IsAnimating(_animation))
        {
            Target.SendSignal(new LeaflingSignal<LeaflingState_Standing>());
        }
    }

    protected override void Update()
    {
        base.Update();
        if (HasEnteredActionFrame)
        {
            float t = Target.ProgressOfFirstActionFrame;
            float speed = _speedCurve.Evaluate(t) * _maxSpeed;
            Target.SetHorizontalVelocity(speed * Target.FacingDirection);
        }
        if (!Target.IsTouching(CardinalDirection.Down))
        {
            Target.SendSignal(new LeaflingSignal_FreeFall(FreeFallEntry.Backflip));
        }
    }
}
