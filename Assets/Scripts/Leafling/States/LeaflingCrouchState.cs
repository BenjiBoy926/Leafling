
namespace Leafling
{
    public class LeaflingCrouchState : LeaflingState
    {
        public LeaflingCrouchState(Leafling leafling) : base(leafling) { }

        public override void Enter()
        {
            base.Enter();
            Leafling.SetAnimation(Leafling.Squat);
        }
        protected override void OnStartedJumping()
        {
            base.OnStartedJumping();
            Leafling.SetState(new LeaflingCrouchJumpState(Leafling));
        }
        protected override void OnStoppedCrouching()
        {
            base.OnStoppedCrouching();
            Leafling.SetState(new LeaflingStandingState(Leafling));
        }
    }
}
