namespace Leafling
{
    public class LeaflingSignal_WallSlide : LeaflingSignal_Generic<LeaflingState_WallSlide>
    {
        private CardinalDirection _wallDirection;

        public LeaflingSignal_WallSlide(CardinalDirection wallDirection)
        {
            _wallDirection = wallDirection;
        }
        public override void PrepareNextState(LeaflingStateMachine machine)
        {
            base.PrepareNextState(machine);
            LeaflingState_WallSlide state = machine.GetState(StateType) as LeaflingState_WallSlide;
            state.SetWallDirection(_wallDirection);
        }

    }
}