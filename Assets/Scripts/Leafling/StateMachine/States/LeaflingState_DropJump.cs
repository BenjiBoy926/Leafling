using UnityEngine;

namespace Leafling
{
    public class LeaflingState_DropJump : LeaflingState
    {
        [SerializeField]
        private SpriteAnimation _animation;
        [SerializeField]
        private float _speed = 30;
        [SerializeField]
        private DirectionalAirControl _airControl;

        protected override void OnEnable()
        {
            base.OnEnable();
            Leafling.SetVerticalVelocity(_speed);
            Leafling.SetAnimation(_animation);
        }
        protected override void OnAnimationFinished()
        {
            base.OnAnimationFinished();
            Leafling.SendSignal(new LeaflingSignal_FreeFall(FreeFallEntry.Backflip));
        }
        protected override void Update()
        {
            base.Update();
            Leafling.ApplyAirControl(_airControl);
        }
    }
}