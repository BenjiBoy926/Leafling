namespace Leafling
{
    public class LeaflingLandingState : LeaflingState
    {
        public LeaflingLandingState(Leafling leafling) : base(leafling)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Leafling.SetAnimation(Leafling.Squat);
        }
        protected override void OnStartedJumping()
        {
            base.OnStartedJumping();
            Leafling.SetState(new LeaflingJumpSquatState(Leafling));
        }
        protected override void OnStartedAimingDash()
        {
            base.OnStartedAimingDash();
            Leafling.SetState(new LeaflingAimingDashState(Leafling));
        }
        protected override void OnAnimationFinished()
        {
            base.OnAnimationFinished();
            Leafling.SetState(new LeaflingStandingState(Leafling));
        }
    }
}