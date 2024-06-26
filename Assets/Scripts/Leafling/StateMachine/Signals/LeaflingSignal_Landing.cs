namespace Leafling
{
    public class LeaflingSignal_Landing : LeaflingSignal_Generic<LeaflingState_Landing>
    {
        private LeaflingSignal _jumpSignal;

        public LeaflingSignal_Landing(LeaflingSignal jumpSignal)
        {
            _jumpSignal = jumpSignal;
        }
        public override void PrepareNextState(LeaflingStateMachine machine)
        {
            base.PrepareNextState(machine);
            LeaflingState_Landing state = GetState(machine);
            state.SetJumpSignal(_jumpSignal);
        }
    }
}