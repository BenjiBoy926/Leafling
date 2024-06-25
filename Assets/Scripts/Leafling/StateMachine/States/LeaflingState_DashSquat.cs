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

        public LeaflingState_DashSquat(Leafling leafling, Vector2 aim, bool dashOnRicochet) : base(leafling) 
        { 
            _aim = aim;
            _dashOnRicochet = dashOnRicochet;
        }

        public override void Enter()
        {
            base.Enter();
            LeaflingStateTool_Dash.ShowDashPerch(Leafling, _aim);
        }
        protected override void OnAnimationFinished()
        {
            base.OnAnimationFinished();
            Leafling.SetState(new LeaflingState_Dash(Leafling, _aim, _dashOnRicochet));
        }
    }
}