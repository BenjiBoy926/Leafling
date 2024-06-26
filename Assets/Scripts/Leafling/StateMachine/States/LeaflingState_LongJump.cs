namespace Leafling
{
    public class LeaflingState_LongJump : LeaflingState
    {
        public override void Enter()
        {
            base.Enter();
            Leafling.SetHorizontalVelocity(0);
            Leafling.SetAnimation(Leafling.LongJump);
            Leafling.SetGravityScale(Leafling.LongJumpGravityScale);
        }
        public override void Exit()
        {
            base.Exit();
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
        public override void Update_Obsolete(float dt)
        {
            base.Update_Obsolete(dt);
            if (HasEnteredActionFrame)
            {
                Leafling.ApplyAirControl(Leafling.LongJumpAirControl);
            }
        }
    }
}