using NaughtyAttributes;
using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Leafling
{
    [Serializable]
    public class SpriteAnimationFrame
    {
        public const string SpriteRelativePath = nameof(_sprite);

        public Sprite Sprite => _sprite;
        public float Duration => _duration;
        public bool IsTransitionFrame => _isTransitionFrame;
        public bool IsActionFrame => _isActionFrame;

        [SerializeField]
        private Sprite _sprite;
        [SerializeField]
        private float _duration;
        [SerializeField, FormerlySerializedAs("_isSmoothStopFrame")]
        private bool _isTransitionFrame;
        [SerializeField]
        private bool _isActionFrame;
    }
}