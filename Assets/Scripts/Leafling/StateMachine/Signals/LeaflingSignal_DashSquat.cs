using UnityEngine;

namespace Leafling
{
    public class LeaflingSignal_DashSquat : LeaflingSignal_Generic<LeaflingState_DashSquat>
    {
        private Vector2 _aim;
        private bool _dashOnRicochet;

        public LeaflingSignal_DashSquat(Vector2 aim, bool dashOnRichochet)
        {
            _aim = aim;
            _dashOnRicochet = dashOnRichochet;
        }
        public override void PrepareNextState(LeaflingStateMachine machine)
        {
            base.PrepareNextState(machine);
            LeaflingState_DashSquat next = GetState(machine);
            next.SetAim(_aim);
            next.SetDashOnRicochet(_dashOnRicochet);
        }
    }
}