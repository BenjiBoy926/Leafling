using UnityEngine;

public class LeaflingState_Standing : LeaflingState
{
    [SerializeField]
    private SpriteAnimation _idle;
    [SerializeField] 
    private SpriteAnimation _running;
    [SerializeField]
    private AirControl _control;

    protected override void OnEnable()
    {
        base.OnEnable();
        Target.SetAnimation(_idle);
    }

    protected override void OnStartedJumping()
    {
        base.OnStartedJumping();
        Target.SendSignal(new LeaflingSignal<LeaflingState_JumpSquat>());
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
        if (Target.HorizontalDirection != 0)
        {
            Target.SendSignal(new LeaflingSignal<LeaflingState_Slide>());
        }
    }

    protected override void Update()
    {
        base.Update();
        Target.ApplyAirControl(_control);
        if (!Target.IsTouching(CardinalDirection.Down))
        {
            Target.SendSignal(new LeaflingSignal_FreeFall(FreeFallEntry.Backflip));
        }
        if (Target.IsCrouching && Target.HorizontalDirection == 0)
        {
            Target.SendSignal(new LeaflingSignal<LeaflingState_Crouch>());
        }
    }
}
