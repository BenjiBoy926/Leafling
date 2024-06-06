namespace Leafling
{
    public class LeaflingJumpSquatState : LeaflingState
    {
        public LeaflingJumpSquatState(Leafling leafling) : base(leafling)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Leafling.SetTransition(new(Leafling.Squat, Leafling.JumpTransitionScale, Leafling.CurrentFlipX));
        }
        protected override void OnAnimationFinished()
        {
            base.OnAnimationFinished();
            if (Leafling.IsAnimating(Leafling.Squat))
            {
                Leafling.SetState(new LeaflingJumpState(Leafling));
            }
        }
    }
}