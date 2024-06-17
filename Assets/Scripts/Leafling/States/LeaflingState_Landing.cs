namespace Leafling
{
    public class LeaflingState_Landing : LeaflingState
    {
        private JumpFromLanding _jumpType;

        public LeaflingState_Landing(Leafling leafling, JumpFromLanding jumpType) : base(leafling)
        {
            _jumpType = jumpType;
        }

        public override void Enter()
        {
            base.Enter();
            Leafling.SetAnimation(Leafling.Squat);
            Leafling.RestoreAbilityToDash();
        }
        protected override void OnStartedJumping()
        {
            base.OnStartedJumping();
            if (_jumpType == JumpFromLanding.Normal)
            {
                Leafling.SetState(new LeaflingState_Jump(Leafling));
            }
            if (_jumpType == JumpFromLanding.CrouchJump)
            {
                Leafling.SetState(new LeaflingState_CrouchJump(Leafling));
            }
        }
        protected override void OnStartedAimingDash()
        {
            base.OnStartedAimingDash();
            if (Leafling.IsAbleToDash)
            {
                Leafling.SetState(new LeaflingState_DashAim(Leafling));
            }
        }
        protected override void OnAnimationFinished()
        {
            base.OnAnimationFinished();
            Leafling.SetState(new LeaflingState_Standing(Leafling));
        }
    }
}