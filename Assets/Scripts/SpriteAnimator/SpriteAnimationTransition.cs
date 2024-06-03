using System;
using UnityEngine;

namespace Leafling
{
    [Serializable]
    public struct SpriteAnimationTransition
    {
        public static readonly SpriteAnimationTransition Empty = new SpriteAnimationTransition();
        public readonly bool IsEmpty => _animation == null;
        public readonly SpriteAnimation Animation => _animation;
        public readonly float Scale => _scale;

        [SerializeField]
        private SpriteAnimation _animation;
        [SerializeField]
        private float _scale;
        [SerializeField]
        private bool _flipX;
    
        public SpriteAnimationTransition(SpriteAnimation animation, float scale, bool flipX)
        {
            _animation = animation;
            _scale = scale;
            _flipX = flipX;
        }
        public readonly void ApplyFlip(SpriteBody body)
        {
            body.SetFlipX(_flipX);
        }
    }
}