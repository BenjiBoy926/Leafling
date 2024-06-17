namespace Leafling
{
    public class LeaflingStateTool_WallJump
    {
        public static void CheckTransitionToWallSlide(Leafling leafling)
        {
            CheckTransitionToWallSlide(leafling, CardinalDirection.Left);
            CheckTransitionToWallSlide(leafling, CardinalDirection.Right);
        }
        private static void CheckTransitionToWallSlide(Leafling leafling, CardinalDirection wallDirection)
        {
            if (ShouldSlideOnWallInDirection(leafling, wallDirection))
            {
                leafling.SetState(new LeaflingState_WallSlide(leafling, wallDirection));
            }
        }
        private static bool ShouldSlideOnWallInDirection(Leafling leafling, CardinalDirection direction)
        {
            return leafling.IsTouching(direction) && leafling.HorizontalDirection == direction.X;
        }
        public static bool WallDirectionToFlipX(CardinalDirection wallDirection)
        {
            return Leafling.DirectionToFlipX(WallDirectionToFacingDirection(wallDirection));
        }
        public static int WallDirectionToFacingDirection(CardinalDirection direction)
        {
            return -direction.X;
        }
    }
}