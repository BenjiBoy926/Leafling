namespace Leafling
{
    public class LeaflingState_CrouchJump : LeaflingState
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            Leafling.SetVerticalVelocity(Leafling.CrouchJumpSpeed);
            Leafling.SetAnimation(Leafling.CrouchJump);
        }
        protected override void OnAnimationFinished()
        {
            base.OnAnimationFinished();
            Leafling.SendSignal(new LeaflingSignal_FreeFall(FreeFallEntry.Backflip));
        }
        protected override void Update()
        {
            base.Update();
            Leafling.ApplyAirControl(Leafling.CrouchJumpAirControl);
        }
    }
}