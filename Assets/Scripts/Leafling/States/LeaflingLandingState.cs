namespace Leafling
{
    public class LeaflingLandingState : LeaflingState
    {
        private JumpFromLanding _jumpType;

        public LeaflingLandingState(Leafling leafling, JumpFromLanding jumpType) : base(leafling)
        {
            _jumpType = jumpType;
        }

        public override void Enter()
        {
            base.Enter();
            Leafling.SetAnimation(Leafling.Squat);
        }
        protected override void OnStartedJumping()
        {
            base.OnStartedJumping();
            if (_jumpType == JumpFromLanding.Normal)
            {
                Leafling.SetState(new LeaflingJumpState(Leafling));
            }
            if (_jumpType == JumpFromLanding.CrouchJump)
            {
                Leafling.SetState(new LeaflingCrouchJumpState(Leafling));
            }
        }
        protected override void OnStartedAimingDash()
        {
            base.OnStartedAimingDash();
            Leafling.SetState(new LeaflingDashAimState(Leafling));
        }
        protected override void OnAnimationFinished()
        {
            base.OnAnimationFinished();
            Leafling.SetState(new LeaflingStandingState(Leafling));
        }
    }
}