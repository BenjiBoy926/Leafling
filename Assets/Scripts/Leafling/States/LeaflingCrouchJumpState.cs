namespace Leafling
{
    public class LeaflingCrouchJumpState : LeaflingState
    {
        public LeaflingCrouchJumpState(Leafling leafling) : base(leafling)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Leafling.SetVerticalVelocity(Leafling.CrouchJumpSpeed);
            Leafling.SetAnimation(Leafling.CrouchJump);
        }
        protected override void OnAnimationFinished()
        {
            base.OnAnimationFinished();
            Leafling.SetState(new LeaflingFreeFallState(Leafling, FreeFallEntry.Normal));
        }
        public override void Update(float dt)
        {
            base.Update(dt);
            Leafling.ApplyAirControl(Leafling.CrouchJumpAirControl);
        }
    }
}