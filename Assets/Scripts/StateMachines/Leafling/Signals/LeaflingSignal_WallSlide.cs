public class LeaflingSignal_WallSlide : LeaflingSignal<LeaflingState_WallSlide>
{
    private CardinalDirection _wallDirection;

    public LeaflingSignal_WallSlide(CardinalDirection wallDirection)
    {
        _wallDirection = wallDirection;
    }
    protected override void PrepareNextState(LeaflingState_WallSlide state)
    {
        base.PrepareNextState(state);
        state.SetWallDirection(_wallDirection);
    }
}
