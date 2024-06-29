using UnityEngine;
using NaughtyAttributes;

namespace Leafling
{
    public class LeaflingState_DashCancel : LeaflingState
    {
        [SerializeField]
        private float _speed;
        [SerializeField, ReadOnly]
        private Vector2 _direction;

        public void SetDirection(Vector2 direction)
        {
            _direction = direction; 
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            LeaflingStateTool_Dash.ShowDashPerch(Leafling, _direction);
        }
        protected override void OnAnimationFinished()
        {
            base.OnAnimationFinished();
            Leafling.SetVelocity(_direction.normalized * _speed);
            Leafling.SendSignal(new LeaflingSignal_FreeFall(FreeFallEntry.Backflip));
        }

        protected override void Update()
        {
            base.Update();
            Leafling.SetVelocity(Vector2.zero);
        }
    }
}