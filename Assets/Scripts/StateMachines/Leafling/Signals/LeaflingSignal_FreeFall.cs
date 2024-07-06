namespace Leafling
{
    public class LeaflingSignal_FreeFall : LeaflingSignal<LeaflingState_FreeFall>
    {
        private FreeFallEntry _entry;

        public LeaflingSignal_FreeFall(FreeFallEntry entry)
        {
            _entry = entry;
        }
        protected override void PrepareNextState(LeaflingState_FreeFall state)
        {
            base.PrepareNextState(state);
            state.SetEntry(_entry);
        }
    }
}