
namespace Leafling
{
    public class LeaflingState_Crouch : LeaflingState
    {
        public LeaflingState_Crouch(Leafling leafling) : base(leafling) { }

        public override void Enter()
        {
            base.Enter();
            Leafling.SetAnimation(Leafling.Squat);
        }
        protected override void OnStartedJumping()
        {
            base.OnStartedJumping();
            Leafling.SendSignal(new LeaflingSignal_Generic<LeaflingState_CrouchJump>());
        }
        protected override void OnStoppedCrouching()
        {
            base.OnStoppedCrouching();
            Leafling.SendSignal(new LeaflingSignal_Generic<LeaflingState_Standing>());
        }
    }
}
