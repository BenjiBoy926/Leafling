using System.ComponentModel;

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

        protected override void OnHorizontalDirectionChanged()
        {
            base.OnHorizontalDirectionChanged();
            TransitionFreeFallAnimation();
        }
        protected override void OnStartedJumping()
        {
            base.OnStartedJumping();
            Leafling.SetState(new LeaflingFlutterState(Leafling));
        }
        protected override void OnStartedAimingDash()
        {
            base.OnStartedAimingDash();
            Leafling.SetState(new LeaflingDashAimState(Leafling));
        }
        protected override void OnStartedCrouching()
        {
            base.OnStartedCrouching();
            Leafling.SetState(new LeaflingDropState(Leafling));
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            Leafling.ApplyAirControl(Leafling.FreeFallAirControl);
            if (Leafling.IsTouching(CardinalDirection.Down))
            {
                Leafling.SetState(new LeaflingLandingState(Leafling));
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