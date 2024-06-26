
namespace Leafling
{
    public class LeaflingState_Crouch : LeaflingState
    {
        public override void Enter()
        {
            base.Enter();
            Leafling.SetAnimation(Leafling.Squat);
        }
        protected override void OnStartedJumping()
        {
            base.OnStartedJumping();
            Leafling.SendSignal(new LeaflingSignal<LeaflingState_CrouchJump>());
        }
        protected override void OnStoppedCrouching()
        {
            base.OnStoppedCrouching();
            Leafling.SendSignal(new LeaflingSignal<LeaflingState_Standing>());
        }
    }
}
