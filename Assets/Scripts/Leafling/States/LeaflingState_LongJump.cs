namespace Leafling
{
    public class LeaflingState_LongJump : LeaflingState
    {
        public LeaflingState_LongJump(Leafling leafling) : base(leafling)
        {
        }

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
                Leafling.SetState(new LeaflingState_Slide(Leafling));
            }
            else
            {
                Leafling.SetState(new LeaflingState_Landing(Leafling, JumpFromLanding.Normal));
            }
        }
        protected override void OnAnimationEnteredActionFrame()
        {
            base.OnAnimationEnteredActionFrame();
            Leafling.SetHorizontalVelocity(Leafling.LongJumpTopSpeed * Leafling.FacingDirection);
        }
        public override void Update(float dt)
        {
            base.Update(dt);
            if (HasEnteredActionFrame)
            {
                Leafling.ApplyAirControl(Leafling.LongJumpAirControl);
            }
        }
    }
}