using UnityEngine;

namespace Leafling
{
    public class LeaflingSignal_Dash : LeaflingSignal_Generic<LeaflingState_Dash>
    {
        private Vector2 _aim;
        private bool _dashOnRicochet;

        public LeaflingSignal_Dash(Vector2 aim, bool dashOnRichochet)
        {
            _aim = aim;
            _dashOnRicochet = dashOnRichochet;
        }
        public override void PrepareNextState(LeaflingStateMachine machine)
        {
            base.PrepareNextState(machine);
            LeaflingState_Dash next = GetState(machine);
            next.SetAim(_aim);
            next.SetDashOnRicochet(_dashOnRicochet);
        }
    }
}