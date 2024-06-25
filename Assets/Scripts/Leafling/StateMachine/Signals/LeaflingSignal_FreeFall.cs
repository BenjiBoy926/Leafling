namespace Leafling
{
    public class LeaflingSignal_FreeFall : LeaflingSignal_Generic<LeaflingState_FreeFall>
    {
        private FreeFallEntry _entry;

        public LeaflingSignal_FreeFall(FreeFallEntry entry)
        {
            _entry = entry;
        }
        public override void PrepareNextState(LeaflingStateMachine machine)
        {
            base.PrepareNextState(machine);
            LeaflingState_FreeFall state = machine.GetState(StateType) as LeaflingState_FreeFall;
            state.SetEntry(_entry);
        }
    }
}