namespace Leafling
{
    public class LeaflingState_Slide : LeaflingState
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            Leafling.SetTransition(new(Leafling.Slide, Leafling.SlideTransitionScale, Leafling.CurrentFlipX));
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
            if (Leafling.IsAnimating(Leafling.Slide))
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
                float speed = Leafling.EvaluateSlideSpeedCurve(t) * Leafling.MaxSlideSpeed;
                Leafling.SetHorizontalVelocity(speed * Leafling.FacingDirection);
            }
            if (!Leafling.IsTouching(CardinalDirection.Down))
            {
                Leafling.SendSignal(new LeaflingSignal_FreeFall(FreeFallEntry.Backflip));
            }
        }
    }
}