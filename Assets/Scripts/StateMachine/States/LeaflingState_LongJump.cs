using UnityEngine;

namespace Leafling
{
    public class LeaflingState_LongJump : LeaflingState
    {
        private float TopSpeed => _airControl.ForwardTopSpeed;

        [SerializeField]
        private SpriteAnimation _animation;
        [SerializeField]
        private float _gravityScale = 0.1f;
        [SerializeField]
        private DirectionalAirControl _airControl;

        protected override void OnEnable()
        {
            base.OnEnable();
            Leafling.SetHorizontalVelocity(0);
            Leafling.SetAnimation(_animation);
            Leafling.SetGravityScale(_gravityScale);
        }
        protected override void OnDisable()
        {
            base.OnDisable();
            Leafling.ResetGravityScale();
        }
        protected override void OnAnimationFinished()
        {
            base.OnAnimationFinished();
            if (Leafling.IsCrouching)
            {
                Leafling.SendSignal(new LeaflingSignal<LeaflingState_Slide>());
            }
            else
            {
                Leafling.SendSignal(new LeaflingSignal_Landing(new LeaflingSignal<LeaflingState_Jump>()));
            }
        }
        protected override void OnAnimationEnteredActionFrame()
        {
            base.OnAnimationEnteredActionFrame();
            Leafling.SetHorizontalVelocity(TopSpeed * Leafling.FacingDirection);
        }
        protected override void Update()
        {
            base.Update();
            if (HasEnteredActionFrame)
            {
                Leafling.ApplyAirControl(_airControl);
            }
        }
    }
}