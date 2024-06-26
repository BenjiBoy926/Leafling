using UnityEngine;

namespace Leafling
{
    public class LeaflingState_WallJump : LeaflingState
    {
        [SerializeField]
        private SpriteAnimation _animation;
        [SerializeField]
        private float _speed = 30;
        [SerializeField]
        private float _exitHopSpeed = 20;
        [SerializeField]
        private float _maxDuration = 1;
        private CardinalDirection _wallDirection;

        public void SetWallDirection(CardinalDirection wallDirection)
        {
            _wallDirection = wallDirection;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            Leafling.SetAnimation(_animation);
            Leafling.FaceTowards(LeaflingStateTool_WallJump.WallDirectionToFacingDirection(_wallDirection));
        }
        protected override void Update()
        {
            base.Update();
            float direction = LeaflingStateTool_WallJump.WallDirectionToFacingDirection(_wallDirection);
            Vector2 velocity = new Vector2(direction, 1).normalized * _speed;
            Leafling.SetVelocity(velocity);
            if (ShouldFinishWallJump())
            {
                FinishWallJump();
            }
            if (Leafling.IsTouching(_wallDirection.Opposite))
            {
                Leafling.SendSignal(new LeaflingSignal_WallSlide(_wallDirection.Opposite, 0.5f));
            }
        }
        private bool ShouldFinishWallJump()
        {
            return !Leafling.IsTouching(_wallDirection) && (!Leafling.IsJumping || TimeSinceStateStart > _maxDuration);
        }
        private void FinishWallJump()
        {
            Leafling.SetVerticalVelocity(_exitHopSpeed);
            Leafling.SendSignal(new LeaflingSignal_FreeFall(FreeFallEntry.Backflip));
        }
    }
}