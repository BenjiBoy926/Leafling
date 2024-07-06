using UnityEngine;

namespace Leafling
{
    public class LeaflingState_Drop : LeaflingState
    {
        [SerializeField]
        private SpriteAnimation _animation;
        [SerializeField]
        private float _animationTransitionScale = 0.1f;
        [SerializeField]
        private float _dropSpeed = 30;
        [SerializeField]
        private float _cancelSpeed = 10;
        [SerializeField]
        private DirectionalAirControl _airControl;

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
                Leafling.SetVerticalVelocity(_cancelSpeed);
            }
            Leafling.SendSignal(new LeaflingSignal_FreeFall(FreeFallEntry.Backflip));
        }

        protected override void Update()
        {
            base.Update();
            Leafling.ApplyAirControl(_airControl);
            if (HasEnteredActionFrame)
            {
                Leafling.SetVerticalVelocity(-_dropSpeed);
            }
            else
            {
                Leafling.SetVerticalVelocity(0);
            }
            if (Leafling.IsTouching(CardinalDirection.Down))
            {
                Leafling.SendSignal(new LeaflingSignal_Landing(new LeaflingSignal<LeaflingState_DropJump>()));
            }
        }
    }
}