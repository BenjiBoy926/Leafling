namespace Leafling
{
    public class LeaflingFreeFallState : LeaflingState
    {
        private FreeFallEntry _entry;

        public LeaflingFreeFallState(Leafling leafling, FreeFallEntry entry) : base(leafling)
        {
            _entry = entry;
        }

        public override void Enter()
        {
            base.Enter();
            Leafling.HorizontalDirectionChanged += OnLeaflingHorizontalDirectionChanged;
            Leafling.StartedJumping += OnLeaflingStartedJumping;
            Leafling.StartedAimingDash += OnLeaflingStartedAimingDash;
            Leafling.StartedCrouching += OnLeaflingStartedCrouching;
            if (_entry == FreeFallEntry.Backflip)
            {
                Leafling.SetAnimation(Leafling.Backflip);
                TransitionFreeFallAnimation();
            }
            else
            {
                Leafling.SetAnimation(Leafling.FreeFallStraight);
            }
        }
        public override void Exit()
        {
            base.Exit();
            Leafling.HorizontalDirectionChanged -= OnLeaflingHorizontalDirectionChanged;
            Leafling.StartedJumping -= OnLeaflingStartedJumping;
            Leafling.StartedAimingDash -= OnLeaflingStartedAimingDash;
            Leafling.StartedCrouching -= OnLeaflingStartedCrouching;
        }

        private void OnLeaflingHorizontalDirectionChanged()
        {
            TransitionFreeFallAnimation();
        }
        private void OnLeaflingStartedJumping()
        {
            Leafling.SetState(new LeaflingFlutterState(Leafling));
        }
        private void OnLeaflingStartedAimingDash()
        {
            Leafling.SetState(new LeaflingAimingDashState(Leafling));
        }
        private void OnLeaflingStartedCrouching()
        {
            Leafling.SetState(new LeaflingDropState(Leafling));
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            Leafling.ApplyFreeFallAirControl();
            if (Leafling.IsTouching(CardinalDirection.Down))
            {
                Leafling.SetState(new LeaflingStandingState(Leafling));
            }
        }

        private void TransitionFreeFallAnimation()
        {
            if (Leafling.HorizontalDirection == 0)
            {
                Leafling.SetTransition(new(Leafling.FreeFallStraight, 1, Leafling.CurrentFlipX));
            }
            else if (Leafling.HorizontalDirection != Leafling.FacingDirection)
            {
                Leafling.SetTransition(new(Leafling.FreeFallBack, 1, Leafling.CurrentFlipX));
            }
            else
            {
                Leafling.SetTransition(new(Leafling.FreeFallForward, 1, Leafling.CurrentFlipX));
            }
        }
    }
}