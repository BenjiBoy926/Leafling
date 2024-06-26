using UnityEngine;

namespace Leafling
{
    public class LeaflingState_WallSlide : LeaflingState
    {
        private bool ShouldForceSlide => TimeSinceStateStart < _forceSlideWindow;

        [SerializeField]
        private float _animationTransitionScale = 0.25f;
        [SerializeField]
        private float _gravityScale = 0.1f;
        private CardinalDirection _wallDirection;
        private float _forceSlideWindow;

        public void SetWallDirection(CardinalDirection wallDirection)
        {
            _wallDirection = wallDirection;
        }
        public void SetForceSlideWindow(float forceSlideWindow)
        {
            _forceSlideWindow = forceSlideWindow;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            bool flip = LeaflingStateTool_WallJump.WallDirectionToFlipX(_wallDirection);
            Leafling.SetTransition(new(Leafling.WallPerch, _animationTransitionScale, flip));
            Leafling.SetGravityScale(_gravityScale);
            Leafling.SetVerticalVelocity(0);
        }
        protected override void OnDisable()
        {
            base.OnDisable();
            Leafling.ResetGravityScale();
        }
        protected override void OnStartedJumping()
        {
            base.OnStartedJumping();
            Leafling.SendSignal(new LeaflingSignal_WallJump(_wallDirection));
        }

        protected override void Update()
        {
            base.Update();
            if (ShouldDisengage())
            {
                Disengage();
            }
            if (Leafling.IsTouching(CardinalDirection.Down) || !Leafling.IsTouching(_wallDirection))
            {
                Leafling.SendSignal(new LeaflingSignal_FreeFall(FreeFallEntry.Normal));
            }
        }
        private void Disengage()
        {
            Leafling.SetHorizontalVelocity(-_wallDirection.X * 5);
            Leafling.SendSignal(new LeaflingSignal_FreeFall(FreeFallEntry.Backflip));
        }
        private bool ShouldDisengage()
        {
            return Leafling.IsCrouching || (!ShouldForceSlide && Leafling.HorizontalDirection != _wallDirection.X);
        }
    }
}