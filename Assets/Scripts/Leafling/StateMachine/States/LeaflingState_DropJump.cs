namespace Leafling
{
    public class LeaflingState_DropJump : LeaflingState
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            Leafling.SetVerticalVelocity(Leafling.DropJumpSpeed);
            Leafling.SetAnimation(Leafling.DropJump);
        }
        protected override void OnAnimationFinished()
        {
            base.OnAnimationFinished();
            Leafling.SendSignal(new LeaflingSignal_FreeFall(FreeFallEntry.Backflip));
        }
        protected override void Update()
        {
            base.Update();
            Leafling.ApplyAirControl(Leafling.DropJumpAirControl);
        }
    }
}