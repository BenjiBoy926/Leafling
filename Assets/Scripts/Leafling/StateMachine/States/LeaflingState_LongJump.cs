namespace Leafling
{
    public class LeaflingState_LongJump : LeaflingState
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            Leafling.SetHorizontalVelocity(0);
            Leafling.SetAnimation(Leafling.LongJump);
            Leafling.SetGravityScale(Leafling.LongJumpGravityScale);
        }
        protected override void OnDisable()
        {
            base.OnDisable();
            Leafling.ResetGravityScale();
        }
        protected override void OnAnimationFinished()
        {
            base.OnAnimationFinished();
            if (Leafling.IsCrouching)
            {
                Leafling.SendSignal(new LeaflingSignal<LeaflingState_Slide>());
            }
            else
            {
                Leafling.SendSignal(new LeaflingSignal_Landing(new LeaflingSignal<LeaflingState_Jump>()));
            }
        }
        protected override void OnAnimationEnteredActionFrame()
        {
            base.OnAnimationEnteredActionFrame();
            Leafling.SetHorizontalVelocity(Leafling.LongJumpTopSpeed * Leafling.FacingDirection);
        }
        protected override void Update()
        {
            base.Update();
            if (HasEnteredActionFrame)
            {
                Leafling.ApplyAirControl(Leafling.LongJumpAirControl);
            }
        }
    }
}