namespace Leafling
{
    public class LeaflingState_Landing : LeaflingState
    {
        private ILeaflingSignal _jumpSignal;

        public void SetJumpSignal(ILeaflingSignal jumpSignal)
        {
            _jumpSignal = jumpSignal;
        }

        public LeaflingState_Landing(Leafling leafling, ILeaflingSignal jumpSignal) : base(leafling)
        {
            _jumpSignal = jumpSignal;
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
            Leafling.SendSignal(_jumpSignal);
        }
        protected override void OnStartedAimingDash()
        {
            base.OnStartedAimingDash();
            if (Leafling.IsAbleToDash)
            {
                Leafling.SendSignal(new LeaflingSignal<LeaflingState_DashAim>());
            }
        }
        protected override void OnAnimationFinished()
        {
            base.OnAnimationFinished();
            Leafling.SendSignal(new LeaflingSignal<LeaflingState_Standing>());
        }
    }
}