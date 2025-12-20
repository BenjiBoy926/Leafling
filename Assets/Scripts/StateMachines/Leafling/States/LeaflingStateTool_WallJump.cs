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
            leafling.SendSignal(new LeaflingSignal_WallSlide(wallDirection));
        }
    }
    private static bool ShouldSlideOnWallInDirection(Leafling leafling, CardinalDirection direction)
    {
        return leafling.IsTouching(direction) && leafling.HorizontalDirection == direction.X;
    }
    public static int WallDirectionToFacingDirection(CardinalDirection direction)
    {
        return -direction.X;
    }
}
