namespace Leafling
{
    public class LeaflingState_JumpSquat : LeaflingState
    {
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
                Leafling.SendSignal(new LeaflingSignal<LeaflingState_Jump>());
            }
        }
    }
}