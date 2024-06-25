using UnityEngine;

namespace Leafling
{
    public class LeaflingState_DashCancel : LeaflingState
    {
        private Vector2 _direction;

        public void SetDirection(Vector2 direction)
        {
            _direction = direction; 
        }

        public LeaflingState_DashCancel(Leafling leafling, Vector2 direction) : base(leafling) 
        { 
            _direction = direction;
        }

        public override void Enter()
        {
            base.Enter();
            LeaflingStateTool_Dash.ShowDashPerch(Leafling, _direction);
        }
        protected override void OnAnimationFinished()
        {
            base.OnAnimationFinished();
            Leafling.SetVelocity(_direction.normalized * Leafling.DashCancelSpeed);
            Leafling.SetState(new LeaflingState_FreeFall(Leafling, FreeFallEntry.Backflip));
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            Leafling.SetVelocity(Vector2.zero);
        }
    }
}