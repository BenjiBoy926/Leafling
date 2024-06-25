namespace Leafling
{
    public class LeaflingState_DropJump : LeaflingState
    {
        public LeaflingState_DropJump(Leafling leafling) : base(leafling)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Leafling.SetVerticalVelocity(Leafling.DropJumpSpeed);
            Leafling.SetAnimation(Leafling.DropJump);
        }
        protected override void OnAnimationFinished()
        {
            base.OnAnimationFinished();
            Leafling.SendSignal(new LeaflingSignal_FreeFall(FreeFallEntry.Backflip));
        }
        public override void Update(float dt)
        {
            base.Update(dt);
            Leafling.ApplyAirControl(Leafling.DropJumpAirControl);
        }
    }
}