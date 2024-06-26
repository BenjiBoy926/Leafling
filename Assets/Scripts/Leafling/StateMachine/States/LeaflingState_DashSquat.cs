using UnityEngine; 

namespace Leafling
{
    public class LeaflingState_DashSquat : LeaflingState
    {
        private Vector2 _aim;
        private bool _dashOnRicochet;

        public void SetAim(Vector2 aim)
        {
            _aim = aim;
        }
        public void SetDashOnRicochet(bool dashOnRicochet)
        {
            _dashOnRicochet = dashOnRicochet;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            LeaflingStateTool_Dash.ShowDashPerch(Leafling, _aim);
        }
        protected override void OnAnimationFinished()
        {
            base.OnAnimationFinished();
            Leafling.SendSignal(new LeaflingSignal_Dash(_aim, _dashOnRicochet));
        }
    }
}