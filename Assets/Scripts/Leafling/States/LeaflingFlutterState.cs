namespace Leafling
{
    public class LeaflingFlutterState : LeaflingState
    {
        private bool _hasEnteredActionFrame = false;

        public LeaflingFlutterState(Leafling leafling) : base(leafling) { }

        public override void Enter()
        {
            base.Enter();
            Leafling.SetTransition(new SpriteAnimationTransition(Leafling.Flutter, Leafling.FlutterTransitionScale, Leafling.CurrentFlipX));
        }
        protected override void OnAnimationFinished()
        {
            base.OnAnimationFinished();
            if (Leafling.IsAnimating(Leafling.Flutter))
            {
                Leafling.SetState(new LeaflingFreeFallState(Leafling, FreeFallEntry.Normal));
            }
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            HandleActionFrameEntry();
            ApplyAirControl();
            if (Leafling.IsTouching(CardinalDirection.Down))
            {
                Leafling.SetState(new LeaflingStandingState(Leafling));
            }
        }
        private void HandleActionFrameEntry()
        {
            if (!Leafling.IsCurrentFrameActionFrame)
            {
                return;
            }
            if (!_hasEnteredActionFrame)
            {
                Leafling.SetVerticalVelocity(5);
            }
            _hasEnteredActionFrame = true;
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