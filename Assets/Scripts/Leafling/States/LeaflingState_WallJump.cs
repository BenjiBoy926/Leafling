using UnityEngine;

namespace Leafling
{
    public class LeaflingState_WallJump : LeaflingState
    {
        private CardinalDirection _wallDirection;

        public LeaflingState_WallJump(Leafling leafling, CardinalDirection wallDirection) : base(leafling)
        {
            _wallDirection = wallDirection;
        }

        public override void Enter()
        {
            base.Enter();
            Leafling.SetAnimation(Leafling.WallJump);
            Leafling.FaceTowards(LeaflingWallJumpTools.WallDirectionToFacingDirection(_wallDirection));
        }
        public override void Update(float dt)
        {
            base.Update(dt);
            float direction = LeaflingWallJumpTools.WallDirectionToFacingDirection(_wallDirection);
            Vector2 velocity = new Vector2(direction, 1).normalized * Leafling.WallJumpSpeed;
            Leafling.SetVelocity(velocity);
            if (ShouldFinishWallJump())
            {
                FinishWallJump();
            }
        }
        private bool ShouldFinishWallJump()
        {
            return !Leafling.IsTouching(_wallDirection) && (!Leafling.IsJumping || TimeSinceStateStart > Leafling.MaxWallJumpTime);
        }
        private void FinishWallJump()
        {
            Leafling.SetVerticalVelocity(Leafling.WallJumpExitHop);
            Leafling.SetState(new LeaflingState_FreeFall(Leafling, FreeFallEntry.Backflip));
        }
    }
}