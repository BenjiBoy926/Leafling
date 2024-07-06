using System;
using System.Collections;
using UnityEngine;

namespace Leafling
{
    public class LeaflingState_DashSpin : LeaflingState
    {
        private Vector2 EndPosition => _target.Position;

        [SerializeField]
        private SpriteAnimation _animation;
        [SerializeField]
        private AnimationCurve _moveToTargetCurve;
        [SerializeField]
        private float _exitHop = 30;
        private DashTarget _target;
        private Vector2 _startPosition;

        public void SetTarget(DashTarget target)
        {
            _target = target;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            Leafling.SetAnimation(_animation);
            Leafling.RestoreAbilityToDash();
            _startPosition = Leafling.GetPosition();
        }
        protected override void OnDisable()
        {
            base.OnDisable();
            Leafling.SetVerticalVelocity(_exitHop);
            Leafling.FaceTowards(-Leafling.FacingDirection);
        }
        protected override void Update()
        {
            base.Update();
            float t = _moveToTargetCurve.Evaluate(Leafling.CurrentAnimationProgress);
            Vector2 position = Vector2.Lerp(_startPosition, EndPosition, t);
            Leafling.SetPosition(position);
        }
        protected override void OnAnimationFinished()
        {
            base.OnAnimationFinished();
            Leafling.SendSignal(new LeaflingSignal_FreeFall(FreeFallEntry.Backflip));
        }
    }
}