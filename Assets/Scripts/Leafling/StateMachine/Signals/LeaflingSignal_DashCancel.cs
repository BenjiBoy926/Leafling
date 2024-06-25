using UnityEngine;

namespace Leafling
{
    public class LeaflingSignal_DashCancel : LeaflingSignal_Generic<LeaflingState_DashCancel>
    {
        private Vector2 _aim;

        public LeaflingSignal_DashCancel(Vector2 aim)
        {
            _aim = aim;
        }
        public override void PrepareNextState(LeaflingStateMachine machine)
        {
            base.PrepareNextState(machine);
            LeaflingState_DashCancel next = machine.GetState(StateType) as LeaflingState_DashCancel;
            next.SetDirection(_aim);
        }
    }
}