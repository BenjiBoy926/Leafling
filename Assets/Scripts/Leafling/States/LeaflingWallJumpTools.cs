namespace Leafling
{
    public class LeaflingWallJumpTools
    {
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