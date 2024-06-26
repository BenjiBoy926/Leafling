using UnityEngine;

namespace Leafling
{
    public class LeaflingState_Slide : LeaflingState
    {
        [SerializeField]
        private SpriteAnimation _animation;
        [SerializeField]
        private float _animationTransitionScale = 0.25f;
        [SerializeField]
        private float _maxSpeed = 30;
        [SerializeField]
        private AnimationCurve _speedCurve;

        protected override void OnEnable()
        {
            base.OnEnable();
            Leafling.SetTransition(new(_animation, _animationTransitionScale, Leafling.CurrentFlipX));
        }

        protected override void OnStartedJumping()
        {
            base.OnStartedJumping();
            if (HasEnteredActionFrame)
            {
                Leafling.SendSignal(new LeaflingSignal<LeaflingState_LongJump>());
            }
        }
        protected override void OnAnimationStarted()
        {
            base.OnAnimationStarted();
            Leafling.SetHorizontalVelocity(0);
        }
        protected override void OnAnimationFinished()
        {
            base.OnAnimationFinished();
            if (Leafling.IsAnimating(_animation))
            {
                Leafling.SendSignal(new LeaflingSignal<LeaflingState_Standing>());
            }
        }

        protected override void Update()
        {
            base.Update();
            if (HasEnteredActionFrame)
            {
                float t = Leafling.ProgressOfFirstActionFrame;
                float speed = _speedCurve.Evaluate(t) * _maxSpeed;
                Leafling.SetHorizontalVelocity(speed * Leafling.FacingDirection);
            }
            if (!Leafling.IsTouching(CardinalDirection.Down))
            {
                Leafling.SendSignal(new LeaflingSignal_FreeFall(FreeFallEntry.Backflip));
            }
        }
    }
}