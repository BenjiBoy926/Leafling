using UnityEngine;

namespace Leafling
{
    public class LeaflingDashCancelState : LeaflingState
    {
        private Vector2 _direction;

        public LeaflingDashCancelState(Leafling leafling, Vector2 direction) : base(leafling) 
        { 
            _direction = direction;
        }

        public override void Enter()
        {
            base.Enter();
            LeaflingDashTools.ShowDashPerch(Leafling, _direction);
        }
        protected override void OnAnimationFinished()
        {
            base.OnAnimationFinished();
            Leafling.SetVelocity(_direction.normalized * Leafling.DashCancelSpeed);
            Leafling.SetState(new LeaflingFreeFallState(Leafling, FreeFallEntry.Backflip));
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            Leafling.SetVelocity(Vector2.zero);
        }
    }
}