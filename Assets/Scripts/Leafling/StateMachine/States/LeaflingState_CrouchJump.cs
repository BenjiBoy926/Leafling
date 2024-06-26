namespace Leafling
{
    public class LeaflingState_CrouchJump : LeaflingState
    {
        public override void Enter()
        {
            base.Enter();
            Leafling.SetVerticalVelocity(Leafling.CrouchJumpSpeed);
            Leafling.SetAnimation(Leafling.CrouchJump);
        }
        protected override void OnAnimationFinished()
        {
            base.OnAnimationFinished();
            Leafling.SendSignal(new LeaflingSignal_FreeFall(FreeFallEntry.Backflip));
        }
        public override void Update_Obsolete(float dt)
        {
            base.Update_Obsolete(dt);
            Leafling.ApplyAirControl(Leafling.CrouchJumpAirControl);
        }
    }
}