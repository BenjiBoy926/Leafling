using System;
using UnityEngine;

namespace Leafling
{
    public class SpriteBody : MonoBehaviour
    {
        public bool FlipX => _renderer.flipX;

        [SerializeField]
        private SpriteRenderer _renderer;

        private void Reset()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }

        public void ShowFrame(SpriteAnimationFrame frame)
        {
            _renderer.sprite = frame.Sprite;
        }
        public void SetFlipX(bool flip)
        {
            _renderer.flipX = flip;
        }
    }
}