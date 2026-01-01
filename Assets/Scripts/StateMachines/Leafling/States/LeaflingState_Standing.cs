using UnityEngine;

public class LeaflingState_Standing : LeaflingState
{
    [SerializeField]
    private SpriteAnimation _idle;
    [SerializeField] 
    private SpriteAnimation _running;
    [SerializeField]
    private PhysicsControl _control;

    protected override void OnEnable()
    {
        base.OnEnable();
        Target.SetAnimation(_idle);
        Target.RestoreAbilityToDash();
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
        Target.ApplyPhysicsControl(_control);
        UpdateAnimation();
        if (!Target.IsTouching(CardinalDirection.Down))
        {
            Target.SendSignal(new LeaflingSignal_FreeFall(FreeFallEntry.Backflip));
        }
        if (Target.IsCrouching && Target.HorizontalDirection == 0)
        {
            Target.SendSignal(new LeaflingSignal<LeaflingState_Crouch>());
        }
    }

    private void UpdateAnimation()
    {
        if (Target.HorizontalDirection == 0 && PhysicsControl.IsNearStationary(Target.PhysicsBody))
        {
            SetAnimation(_idle);
        }
        if (Target.HorizontalDirection != 0)
        {
            SetAnimation(_running);
            Target.FaceTowards(Target.HorizontalDirection);
        }
    }

    private void SetAnimation(SpriteAnimation animation)
    {
        if (!Target.IsAnimating(animation))
        {
            Target.SetAnimation(animation);
        }
    }
}
