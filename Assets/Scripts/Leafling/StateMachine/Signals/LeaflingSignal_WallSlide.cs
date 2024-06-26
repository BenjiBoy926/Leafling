namespace Leafling
{
    public class LeaflingSignal_WallSlide : LeaflingSignal<LeaflingState_WallSlide>
    {
        private CardinalDirection _wallDirection;
        private float _forceSlideWindow;

        public LeaflingSignal_WallSlide(CardinalDirection wallDirection, float forceSlideWindow)
        {
            _wallDirection = wallDirection;
            _forceSlideWindow = forceSlideWindow;
        }
        protected override void PrepareNextState(LeaflingState_WallSlide state)
        {
            base.PrepareNextState(state);
            state.SetWallDirection(_wallDirection);
            state.SetForceSlideWindow(_forceSlideWindow);
        }
    }
}