namespace Leafling
{
    public class LeaflingLongJumpState : LeaflingState
    {
        public LeaflingLongJumpState(Leafling leafling) : base(leafling)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Leafling.SetHorizontalVelocity(0);
            Leafling.SetAnimation(Leafling.LongJump);
        }
        protected override void OnAnimationFinished()
        {
            base.OnAnimationFinished();
            if (Leafling.IsCrouching)
            {
                Leafling.SetState(new LeaflingSlideState(Leafling));
            }
            else
            {
                Leafling.SetState(new LeaflingLandingState(Leafling));
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