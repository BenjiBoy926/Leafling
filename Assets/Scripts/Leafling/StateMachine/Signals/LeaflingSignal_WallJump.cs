namespace Leafling
{
    public class LeaflingSignal_WallJump : LeaflingSignal_Generic<LeaflingState_WallJump>
    {
        private CardinalDirection _wallDirection;

        public LeaflingSignal_WallJump(CardinalDirection wallDirection)
        {
            _wallDirection = wallDirection;
        }
        public override void PrepareNextState(LeaflingStateMachine machine)
        {
            base.PrepareNextState(machine);
            LeaflingState_WallJump state = machine.GetState(StateType) as LeaflingState_WallJump; ;
            state.SetWallDirection(_wallDirection);
        }
    }
}