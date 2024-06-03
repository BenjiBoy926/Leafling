namespace Leafling
{
    public class LeaflingDropState : LeaflingState
    {
        private bool _hasEnteredActionFrame = false;

        public LeaflingDropState(Leafling leafling) : base(leafling) { }

        public override void Enter()
        {
            base.Enter();
            Leafling.SetTransition(new(Leafling.Drop, 0.3f, Leafling.CurrentFlipX));
            Leafling.StartedJumping += OnStartedJumping;
        }
        public override void Exit()
        {
            base.Exit();
            Leafling.StartedJumping -= OnStartedJumping;
        }

        private void OnStartedJumping()
        {
            Leafling.SetVerticalVelocity(5);
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
                Leafling.SetVerticalVelocity(-Leafling.MaxJumpSpeed);
            }
            if (Leafling.IsTouching(CardinalDirection.Down))
            {
                Leafling.SetState(new LeaflingStandingState(Leafling));
            }
        }
    }
}