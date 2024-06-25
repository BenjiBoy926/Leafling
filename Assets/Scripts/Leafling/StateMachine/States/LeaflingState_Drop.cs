namespace Leafling
{
    public class LeaflingState_Drop : LeaflingState
    {
        public LeaflingState_Drop(Leafling leafling) : base(leafling) { }

        public override void Enter()
        {
            base.Enter();
            Leafling.SetTransition(new(Leafling.Drop, Leafling.DropTransitionScale, Leafling.CurrentFlipX));
        }
        protected override void OnStartedJumping()
        {
            base.OnStartedJumping();
            if (HasEnteredActionFrame)
            {
                Leafling.SetVerticalVelocity(Leafling.DropCancelSpeed);
            }
            Leafling.SendSignal(new LeaflingSignal_FreeFall(FreeFallEntry.Backflip));
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            Leafling.ApplyAirControl(Leafling.DropAirControl);
            if (HasEnteredActionFrame)
            {
                Leafling.SetVerticalVelocity(-Leafling.DropSpeed);
            }
            else
            {
                Leafling.SetVerticalVelocity(0);
            }
            if (Leafling.IsTouching(CardinalDirection.Down))
            {
                Leafling.SendSignal(new LeaflingSignal_Landing(new LeaflingSignal_Generic<LeaflingState_DropJump>()));
            }
        }
    }
}