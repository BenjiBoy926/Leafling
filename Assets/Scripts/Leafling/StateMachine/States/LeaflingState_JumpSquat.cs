using UnityEngine;

namespace Leafling
{
    public class LeaflingState_JumpSquat : LeaflingState
    {
        [SerializeField]
        private float _animationTransitionScale = 0.1f;

        protected override void OnEnable()
        {
            base.OnEnable();
            Leafling.SetTransition(new(Leafling.Squat, _animationTransitionScale, Leafling.CurrentFlipX));
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