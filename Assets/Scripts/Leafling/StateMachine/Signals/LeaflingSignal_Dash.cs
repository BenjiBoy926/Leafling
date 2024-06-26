using UnityEngine;

namespace Leafling
{
    public class LeaflingSignal_Dash : LeaflingSignal<LeaflingState_Dash>
    {
        private Vector2 _aim;
        private bool _dashOnRicochet;

        public LeaflingSignal_Dash(Vector2 aim, bool dashOnRichochet)
        {
            _aim = aim;
            _dashOnRicochet = dashOnRichochet;
        }
        protected override void PrepareNextState(LeaflingState_Dash state)
        {
            base.PrepareNextState(state);
            state.SetAim(_aim);
            state.SetDashOnRicochet(_dashOnRicochet);
        }
    }
}