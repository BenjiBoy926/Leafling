namespace Leafling
{
    public class LeaflingSignal_Landing : LeaflingSignal<LeaflingState_Landing>
    {
        private ILeaflingSignal _jumpSignal;

        public LeaflingSignal_Landing(ILeaflingSignal jumpSignal)
        {
            _jumpSignal = jumpSignal;
        }
        protected override void PrepareNextState(LeaflingState_Landing state)
        {
            base.PrepareNextState(state);
            state.SetJumpSignal(_jumpSignal);
        }
    }
}