namespace Leafling
{
    public class LeaflingState_Landing : LeaflingState
    {
        private LeaflingState _jumpState;

        public LeaflingState_Landing(Leafling leafling) : this(leafling, new LeaflingState_Jump(leafling)) { }
        public LeaflingState_Landing(Leafling leafling, LeaflingState jumpState) : base(leafling)
        {
            _jumpState = jumpState;
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
            Leafling.SetState(_jumpState);
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