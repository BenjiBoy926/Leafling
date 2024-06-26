namespace Leafling
{
    public class LeaflingState_FreeFall : LeaflingState
    {
        private FreeFallEntry _entry;

        public void SetEntry(FreeFallEntry entry)
        {
            _entry = entry;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
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
            Leafling.SendSignal(new LeaflingSignal<LeaflingState_Flutter>());
        }
        protected override void OnStartedAimingDash()
        {
            base.OnStartedAimingDash();
            if (Leafling.IsAbleToDash)
            {
                Leafling.SendSignal(new LeaflingSignal<LeaflingState_DashAim>());
            }
        }
        protected override void OnStartedCrouching()
        {
            base.OnStartedCrouching();
            Leafling.SendSignal(new LeaflingSignal<LeaflingState_Drop>());
        }

        protected override void Update()
        {
            base.Update();
            Leafling.ApplyAirControl(Leafling.FreeFallAirControl);
            if (Leafling.IsTouching(CardinalDirection.Down))
            {
                Leafling.SendSignal(new LeaflingSignal_Landing(new LeaflingSignal<LeaflingState_Jump>()));
            }
            LeaflingStateTool_WallJump.CheckTransitionToWallSlide(Leafling);
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