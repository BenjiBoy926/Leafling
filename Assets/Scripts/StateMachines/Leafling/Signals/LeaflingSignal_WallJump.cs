namespace Leafling
{
    public class LeaflingSignal_WallJump : LeaflingSignal<LeaflingState_WallJump>
    {
        private CardinalDirection _wallDirection;

        public LeaflingSignal_WallJump(CardinalDirection wallDirection)
        {
            _wallDirection = wallDirection;
        }
        protected override void PrepareNextState(LeaflingState_WallJump state)
        {
            base.PrepareNextState(state);
            state.SetWallDirection(_wallDirection);
        }
    }
}