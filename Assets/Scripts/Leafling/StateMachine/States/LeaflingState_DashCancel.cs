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

        public override void Enter()
        {
            base.Enter();
            LeaflingStateTool_Dash.ShowDashPerch(Leafling, _direction);
        }
        protected override void OnAnimationFinished()
        {
            base.OnAnimationFinished();
            Leafling.SetVelocity(_direction.normalized * Leafling.DashCancelSpeed);
            Leafling.SendSignal(new LeaflingSignal_FreeFall(FreeFallEntry.Backflip));
        }

        public override void Update_Obsolete(float dt)
        {
            base.Update_Obsolete(dt);
            Leafling.SetVelocity(Vector2.zero);
        }
    }
}