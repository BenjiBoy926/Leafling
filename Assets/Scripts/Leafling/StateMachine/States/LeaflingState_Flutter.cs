namespace Leafling
{
    public class LeaflingState_Flutter : LeaflingState
    {
        public LeaflingState_Flutter(Leafling leafling) : base(leafling) { }

        public override void Enter()
        {
            base.Enter();
            Leafling.SetTransition(new(Leafling.Flutter, Leafling.FlutterTransitionScale, Leafling.CurrentFlipX));
        }
        protected override void OnStartedAimingDash()
        {
            base.OnStartedAimingDash();
            if (Leafling.IsAbleToDash)
            {
                Leafling.SendSignal(new LeaflingSignal<LeaflingState_DashAim>());
            }
        }
        protected override void OnAnimationFinished()
        {
            base.OnAnimationFinished();
            if (Leafling.IsAnimating(Leafling.Flutter))
            {
                Leafling.SendSignal(new LeaflingSignal_FreeFall(FreeFallEntry.Normal));
            }
        }
        protected override void OnAnimationEnteredActionFrame()
        {
            base.OnAnimationEnteredActionFrame();
            Leafling.SetVerticalVelocity(5);
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            ApplyAirControl();
            if (Leafling.IsTouching(CardinalDirection.Down))
            {
                Leafling.SendSignal(new LeaflingSignal_Landing(new LeaflingSignal<LeaflingState_Jump>()));
            }
            LeaflingStateTool_WallJump.CheckTransitionToWallSlide(Leafling);
        }
        private void ApplyAirControl()
        {
            if (Leafling.IsCurrentFrameActionFrame)
            {
                Leafling.ApplyAirControl(Leafling.FlutterAirControl);
            }
            else
            {
                Leafling.ApplyAirControl(Leafling.FreeFallAirControl);
            }
        }
    }
}