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
            Target.SetHorizontalVelocity(0);
            Target.SetAnimation(_animation);
            Target.SetGravityScale(_gravityScale);
        }
        protected override void OnDisable()
        {
            base.OnDisable();
            Target.ResetGravityScale();
        }
        protected override void OnAnimationFinished()
        {
            base.OnAnimationFinished();
            if (Target.IsCrouching)
            {
                Target.SendSignal(new LeaflingSignal<LeaflingState_Slide>());
            }
            else
            {
                Target.SendSignal(new LeaflingSignal_Landing(new LeaflingSignal<LeaflingState_Jump>()));
            }
        }
        protected override void OnAnimationEnteredActionFrame()
        {
            base.OnAnimationEnteredActionFrame();
            Target.SetHorizontalVelocity(TopSpeed * Target.FacingDirection);
        }
        protected override void Update()
        {
            base.Update();
            if (HasEnteredActionFrame)
            {
                Target.ApplyAirControl(_airControl);
            }
        }
    }
}