using UnityEngine; 

namespace Leafling
{
    public class LeaflingDashSquatState : LeaflingState
    {
        private Vector2 _aim;
        private bool _dashOnRicochet;

        public LeaflingDashSquatState(Leafling leafling, Vector2 aim, bool dashOnRicochet) : base(leafling) 
        { 
            _aim = aim;
            _dashOnRicochet = dashOnRicochet;
        }

        public override void Enter()
        {
            base.Enter();
            LeaflingDashTools.ShowDashPerch(Leafling, _aim);
        }
        protected override void OnAnimationFinished()
        {
            base.OnAnimationFinished();
            Leafling.SetState(new LeaflingDashState(Leafling, _aim, _dashOnRicochet));
        }
    }
}