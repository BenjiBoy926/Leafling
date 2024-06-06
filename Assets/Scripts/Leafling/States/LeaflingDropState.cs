namespace Leafling
{
    public class LeaflingDropState : LeaflingState
    {
        private bool _hasEnteredActionFrame = false;

        public LeaflingDropState(Leafling leafling) : base(leafling) { }

        public override void Enter()
        {
            base.Enter();
            Leafling.SetTransition(new(Leafling.Drop, Leafling.DropTransitionScale, Leafling.CurrentFlipX));
        }
        protected override void OnStartedJumping()
        {
            base.OnStartedJumping();
            if (_hasEnteredActionFrame)
            {
                Leafling.SetVerticalVelocity(Leafling.DropCancelSpeed);
            }
            Leafling.SetState(new LeaflingFreeFallState(Leafling, FreeFallEntry.Backflip));
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            Leafling.ApplyAirControl(Leafling.DropAirControl);
            if (Leafling.IsCurrentFrameActionFrame)
            {
                _hasEnteredActionFrame = true;
            }
            if (!_hasEnteredActionFrame)
            {
                Leafling.SetVerticalVelocity(0);
            }
            else
            {
                Leafling.SetVerticalVelocity(-Leafling.DropSpeed);
            }
            if (Leafling.IsTouching(CardinalDirection.Down))
            {
                Leafling.SetState(new LeaflingLandingState(Leafling));
            }
        }
    }
}