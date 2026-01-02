using UnityEngine;
using NaughtyAttributes;

public class LeaflingState_FreeFall : LeaflingState
{
    [SerializeField]
    private SpriteAnimation _backflip;
    [SerializeField]
    private SpriteAnimation _fallBack;
    [SerializeField]
    private SpriteAnimation _fallDown;
    [SerializeField]
    private SpriteAnimation _fallForward;
    [SerializeField]
    private DirectionalPhysicsControl _backflipAirControl;
    [SerializeField]
    private DirectionalPhysicsControl _airControl;
    [SerializeField, ReadOnly]
    private FreeFallEntry _entry;

    public static void Enter(Leafling target, FreeFallEntry entry)
    {
        LeaflingState_FreeFall state = target.GetState<LeaflingState_FreeFall>();
        state._entry = entry;
        target.SetState(state);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        if (_entry == FreeFallEntry.Backflip)
        {
            Target.SetAnimation(_backflip);
        }
        TransitionFreeFallAnimation();
    }

    protected override void OnHorizontalDirectionChanged()
    {
        base.OnHorizontalDirectionChanged();
        TransitionFreeFallAnimation();
    }
    protected override void OnStartedAimingDash()
    {
        base.OnStartedAimingDash();
        if (Target.IsAbleToDash)
        {
            Target.SetState<LeaflingState_DashAim>();
        }
    }
    protected override void OnStartedCrouching()
    {
        base.OnStartedCrouching();
        Target.SetState<LeaflingState_Drop>();
    }
    protected override void OnStartedJumping()
    {
        base.OnStartedJumping();
        Target.SetState<LeaflingState_Flutter>();
    }
    protected override void OnDashTargetTouched(DashTarget target)
    {
        base.OnDashTargetTouched(target);
        target.Tickle(Target.DashTargeter);
    }

    protected override void Update()
    {
        base.Update();
        if (Target.IsAnimating(_backflip))
        {
            Target.ApplyPhysicsControl(_backflipAirControl);
        }
        else
        {
            Target.ApplyPhysicsControl(_airControl);
        }
        if (Target.IsTouching(CardinalDirection.Down) && Target.VerticalVelocity <= 0)
        {
            Target.SetState<LeaflingState_Standing>();
        }
        LeaflingStateTool_WallJump.CheckTransitionToWallSlide(Target);
    }

    private void TransitionFreeFallAnimation()
    {
        if (Target.HorizontalDirection == 0)
        {
            Target.SetTransition(new(_fallDown, 1, Target.CurrentFlipX));
        }
        else if (Target.HorizontalDirection != Target.FacingDirection)
        {
            Target.SetTransition(new(_fallBack, 1, Target.CurrentFlipX));
        }
        else
        {
            Target.SetTransition(new(_fallForward, 1, Target.CurrentFlipX));
        }
    }
}
