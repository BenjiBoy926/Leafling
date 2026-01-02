public class LeaflingState_DropLanding : LeaflingState
{
    protected override void OnEnable()
    {
        base.OnEnable();
        Target.SetAnimation(Target.Squat);
        Target.RestoreAbilityToDash();
    }
    protected override void OnStartedJumping()
    {
        base.OnStartedJumping();
        Target.SetState<LeaflingState_DropJump>();
    }
    protected override void OnStartedAimingDash()
    {
        base.OnStartedAimingDash();
        if (Target.IsAbleToDash)
        {
            Target.SetState<LeaflingState_DashAim>();
        }
    }
    protected override void OnAnimationFinished()
    {
        base.OnAnimationFinished();
        Target.SetState<LeaflingState_Standing>();
    }
}
