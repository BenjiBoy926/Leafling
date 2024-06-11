namespace Leafling
{
    public class LeaflingWallJumpTools
    {
        public static bool WallDirectionToFlipX(CardinalDirection wallDirection)
        {
            return Leafling.DirectionToFlipX(WallDirectionToFacingDirection(wallDirection));
        }
        public static float WallDirectionToFacingDirection(CardinalDirection direction)
        {
            return -WallDirectionToInt(direction);
        }
        public static int WallDirectionToInt(CardinalDirection wallDirection)
        {
            return (int)wallDirection.ToVector().x;
        }
    }
}