using UnityEngine;

namespace Leafling
{
    public class LeaflingSignal_DashCancel : LeaflingSignal<LeaflingState_DashCancel>
    {
        private Vector2 _aim;

        public LeaflingSignal_DashCancel(Vector2 aim)
        {
            _aim = aim;
        }
        protected override void PrepareNextState(LeaflingState_DashCancel state)
        {
            base.PrepareNextState(state);
            state.SetDirection(_aim);
        }
    }
}