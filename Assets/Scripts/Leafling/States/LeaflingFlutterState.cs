namespace Leafling
{
    public class LeaflingFlutterState : LeaflingState
    {
        public LeaflingFlutterState(Leafling leafling) : base(leafling) { }

        public override void Enter()
        {
            base.Enter();
            Leafling.SetTransition(new(Leafling.Flutter, Leafling.FlutterTransitionScale, Leafling.CurrentFlipX));
        }
        protected override void OnAnimationFinished()
        {
            base.OnAnimationFinished();
            if (Leafling.IsAnimating(Leafling.Flutter))
            {
                Leafling.SetState(new LeaflingFreeFallState(Leafling, FreeFallEntry.Normal));
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
                Leafling.SetState(new LeaflingLandingState(Leafling));
            }
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