namespace Leafling
{
    public class LeaflingState_CrouchJump : LeaflingState
    {
        public LeaflingState_CrouchJump(Leafling leafling) : base(leafling)
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
            Leafling.SetState(new LeaflingState_FreeFall(Leafling, FreeFallEntry.Backflip));
        }
        public override void Update(float dt)
        {
            base.Update(dt);
            Leafling.ApplyAirControl(Leafling.CrouchJumpAirControl);
        }
    }
}