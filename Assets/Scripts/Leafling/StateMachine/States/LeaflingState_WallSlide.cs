namespace Leafling
{
    public class LeaflingState_WallSlide : LeaflingState
    {
        private bool ShouldForceSlide => TimeSinceStateStart < _forceSlideWindow;

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

        public LeaflingState_WallSlide(Leafling leafling, CardinalDirection wallDirection) : this(leafling, wallDirection, 0)
        {

        }
        public LeaflingState_WallSlide(Leafling leafling, CardinalDirection wallDirection, float forceSlideWindow) : base(leafling)
        {
            _wallDirection = wallDirection;
            _forceSlideWindow = forceSlideWindow;
        }

        public override void Enter()
        {
            base.Enter();
            bool flip = LeaflingStateTool_WallJump.WallDirectionToFlipX(_wallDirection);
            Leafling.SetTransition(new(Leafling.WallPerch, Leafling.WallSlideTransitionScale, flip));
            Leafling.SetGravityScale(Leafling.WallSlideGravityScale);
            Leafling.SetVerticalVelocity(0);
        }
        public override void Exit()
        {
            base.Exit();
            Leafling.ResetGravityScale();
        }
        protected override void OnStartedJumping()
        {
            base.OnStartedJumping();
            Leafling.SetState(new LeaflingState_WallJump(Leafling, _wallDirection));
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            if (ShouldDisengage())
            {
                Disengage();
            }
            if (Leafling.IsTouching(CardinalDirection.Down) || !Leafling.IsTouching(_wallDirection))
            {
                Leafling.SetState(new LeaflingState_FreeFall(Leafling, FreeFallEntry.Normal));
            }
        }
        private void Disengage()
        {
            Leafling.SetHorizontalVelocity(-_wallDirection.X * 5);
            Leafling.SetState(new LeaflingState_FreeFall(Leafling, FreeFallEntry.Backflip));
        }
        private bool ShouldDisengage()
        {
            return Leafling.IsCrouching || (!ShouldForceSlide && Leafling.HorizontalDirection != _wallDirection.X);
        }
    }
}