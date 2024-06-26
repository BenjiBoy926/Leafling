namespace Leafling
{
    public class LeaflingSignal_WallSlide : LeaflingSignal_Generic<LeaflingState_WallSlide>
    {
        private CardinalDirection _wallDirection;
        private float _forceSlideWindow;

        public LeaflingSignal_WallSlide(CardinalDirection wallDirection, float forceSlideWindow)
        {
            _wallDirection = wallDirection;
            _forceSlideWindow = forceSlideWindow;
        }
        public override void PrepareNextState(LeaflingStateMachine machine)
        {
            base.PrepareNextState(machine);
            LeaflingState_WallSlide state = GetState(machine);
            state.SetWallDirection(_wallDirection);
            state.SetForceSlideWindow(_forceSlideWindow);
        }
    }
}