namespace Leafling
{
    public class LeaflingDropState : LeaflingState
    {
        public LeaflingDropState(Leafling leafling) : base(leafling) { }

        public override void Enter()
        {
            base.Enter();
            Leafling.SetTransition(new(Leafling.Drop, Leafling.DropTransitionScale, Leafling.CurrentFlipX));
        }
        protected override void OnStartedJumping()
        {
            base.OnStartedJumping();
            if (HasEnteredActionFrame)
            {
                Leafling.SetVerticalVelocity(Leafling.DropCancelSpeed);
            }
            Leafling.SetState(new LeaflingFreeFallState(Leafling, FreeFallEntry.Backflip));
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            Leafling.ApplyAirControl(Leafling.DropAirControl);
            if (HasEnteredActionFrame)
            {
                Leafling.SetVerticalVelocity(-Leafling.DropSpeed);
            }
            else
            {
                Leafling.SetVerticalVelocity(0);
            }
            if (Leafling.IsTouching(CardinalDirection.Down))
            {
                Leafling.SetState(new LeaflingLandingState(Leafling));
            }
        }
    }
}