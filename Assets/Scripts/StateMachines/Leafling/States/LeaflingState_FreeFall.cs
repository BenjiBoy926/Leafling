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
    private DirectionalAirControl _airControl;
    [SerializeField, ReadOnly]
    private FreeFallEntry _entry;

    public void SetEntry(FreeFallEntry entry)
    {
        _entry = entry;
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
            Target.SendSignal(new LeaflingSignal<LeaflingState_DashAim>());
        }
    }
    protected override void OnStartedCrouching()
    {
        base.OnStartedCrouching();
        Target.SendSignal(new LeaflingSignal<LeaflingState_Drop>());
    }
    protected override void OnDashTargetTouched(DashTarget target)
    {
        base.OnDashTargetTouched(target);
        target.Tickle(Target.DashTargeter);
    }

    protected override void Update()
    {
        base.Update();
        Target.ApplyAirControl(_airControl);
        if (Target.IsTouching(CardinalDirection.Down) && Target.VerticalVelocity <= 0)
        {
            Target.SendSignal(new LeaflingSignal_Landing(new LeaflingSignal<LeaflingState_Jump>()));
        }
        LeaflingStateTool_WallJump.CheckTransitionToWallSlide(Target);
        if (Target.IsJumping)
        {
            Target.SendSignal(new LeaflingSignal<LeaflingState_Flutter>());
        }
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
