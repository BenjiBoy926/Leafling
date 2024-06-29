using UnityEngine;
using NaughtyAttributes;

namespace Leafling
{
    public class LeaflingState_FreeFall : LeaflingState
    {
        [SerializeField]
        private SpriteAnimation _backflip;
        [SerializeField]
        private SpriteAnimation _fallBack;
        [SerializeField]
        private SpriteAnimation _fallDown;
        [SerializeField]
        private SpriteAnimation _fallForward;
        [SerializeField]
        private DirectionalAirControl _airControl;
        [SerializeField, ReadOnly]
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
                Leafling.SetAnimation(_backflip);
                TransitionFreeFallAnimation();
            }
            else
            {
                Leafling.SetAnimation(_fallDown);
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
            Leafling.ApplyAirControl(_airControl);
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
                Leafling.SetTransition(new(_fallDown, 1, Leafling.CurrentFlipX));
            }
            else if (Leafling.HorizontalDirection != Leafling.FacingDirection)
            {
                Leafling.SetTransition(new(_fallBack, 1, Leafling.CurrentFlipX));
            }
            else
            {
                Leafling.SetTransition(new(_fallForward, 1, Leafling.CurrentFlipX));
            }
        }
    }
}