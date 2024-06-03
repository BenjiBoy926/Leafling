using NaughtyAttributes;
using UnityEngine;

namespace Leafling
{
    public class SpriteAnimationTransitionTest : MonoBehaviour
    {
        [SerializeField]
        private SpriteAnimator _animator;
        [SerializeField]
        private SpriteAnimation _animation1;
        [SerializeField]
        private SpriteAnimation _animation2;

        [Button]
        private void TransitionToAnimation1()
        {
            _animator.SetTransition(new SpriteAnimationTransition(_animation1, 1, false));
        }
        [Button]
        private void TransitionToAnimation2()
        {
            _animator.SetTransition(new SpriteAnimationTransition(_animation2, 1, false));
        }
    }
}