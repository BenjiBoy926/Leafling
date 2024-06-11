namespace Leafling
{
    public class LeaflingState_WallSlide : LeaflingState
    {
        private CardinalDirection _wallDirection;

        public LeaflingState_WallSlide(Leafling leafling, CardinalDirection wallDirection) : base(leafling)
        {
            _wallDirection = wallDirection;
        }

        public override void Enter()
        {
            base.Enter();
            Leafling.SetTransition(new(Leafling.WallPerch, Leafling.WallSlideTransitionScale, FlipX()));
            Leafling.SetGravityScale(Leafling.WallSlideGravityScale);
            Leafling.SetVerticalVelocity(0);
        }
        public override void Exit()
        {
            base.Exit();
            Leafling.ResetGravityScale();
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            if (ShouldFreeFall())
            {
                Leafling.SetState(new LeaflingState_FreeFall(Leafling, FreeFallEntry.Normal)); 
            }
            if (Leafling.IsTouching(CardinalDirection.Down))
            {
                Leafling.SetState(new LeaflingState_Landing(Leafling, JumpFromLanding.Normal));
            }
        }
        private bool ShouldFreeFall()
        {
            return Leafling.HorizontalDirection != WallDirectionAsInt() || !Leafling.IsTouching(_wallDirection) || Leafling.IsTouching(CardinalDirection.Down);
        }
        private bool FlipX()
        {
            return Leafling.DirectionToFlipX(FacingDirection());
        }
        private float FacingDirection()
        {
            return -WallDirectionAsInt();
        }
        private float WallDirectionAsInt()
        {
            return (int)_wallDirection.ToVector().x;
        }
    }
}