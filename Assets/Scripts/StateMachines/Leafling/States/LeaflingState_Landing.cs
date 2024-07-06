public class LeaflingState_Landing : LeaflingState
{
    private ILeaflingSignal _jumpSignal;

    public void SetJumpSignal(ILeaflingSignal jumpSignal)
    {
        _jumpSignal = jumpSignal;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        Target.SetAnimation(Target.Squat);
        Target.RestoreAbilityToDash();
    }
    protected override void OnStartedJumping()
    {
        base.OnStartedJumping();
        Target.SendSignal(_jumpSignal);
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
        Target.SendSignal(new LeaflingSignal<LeaflingState_Standing>());
    }
}
