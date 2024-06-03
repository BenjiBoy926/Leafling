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
        }
    }
}