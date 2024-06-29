using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Leafling
{
    public class LeaflingState : MonoBehaviour
    {
        protected Leafling Leafling => _leafling;
        protected float TimeSinceStateStart => Time.time - _timeOfStateStart;
        protected bool HasEnteredActionFrame => _hasEnteredActionFrame;

        [SerializeField]
        private Leafling _leafling;
        private float _timeOfStateStart;
        private bool _hasEnteredActionFrame;

        private void Reset()
        {
            _leafling = GetLeafling();
        }
        protected virtual void Awake()
        {
            if (_leafling == null)
            {
                _leafling = GetLeafling();
            }
        }
        private Leafling GetLeafling()
        {
            return GetComponentInParent<Leafling>();
        }

        protected virtual void OnEnable()
        {
            _timeOfStateStart = Time.time;
            _hasEnteredActionFrame = false;
            Leafling.HorizontalDirectionChanged += OnHorizontalDirectionChanged;
            Leafling.StartedJumping += OnStartedJumping;
            Leafling.StoppedJumping += OnStoppedJumping;
            Leafling.StartedAimingDash += OnStartedAimingDash;
            Leafling.StoppedAimingDash += OnStoppedAimingDash;
            Leafling.StartedCrouching += OnStartedCrouching;
            Leafling.StoppedCrouching += OnStoppedCrouching;
            Leafling.AnimationStarted += OnAnimationStarted;
            Leafling.AnimationFinished += OnAnimationFinished;
            Leafling.AnimationEnteredActionFrame += OnAnimationEnteredActionFrame;
            Leafling.DashTargetStruck += OnDashTargetStruck;
        }
        protected virtual void OnDisable()
        {
            Leafling.HorizontalDirectionChanged -= OnHorizontalDirectionChanged;
            Leafling.StartedJumping -= OnStartedJumping;
            Leafling.StoppedJumping -= OnStoppedJumping;
            Leafling.StartedAimingDash -= OnStartedAimingDash;
            Leafling.StoppedAimingDash -= OnStoppedAimingDash;
            Leafling.StartedCrouching -= OnStartedCrouching;
            Leafling.StoppedCrouching -= OnStoppedCrouching;
            Leafling.AnimationStarted -= OnAnimationStarted;
            Leafling.AnimationFinished -= OnAnimationFinished;
            Leafling.AnimationEnteredActionFrame -= OnAnimationEnteredActionFrame;
            Leafling.DashTargetStruck -= OnDashTargetStruck;
        }
        protected virtual void Update()
        {

        }

        protected virtual void OnHorizontalDirectionChanged()
        {

        }
        protected virtual void OnStartedJumping()
        {

        }
        protected virtual void OnStoppedJumping()
        {

        }
        protected virtual void OnStartedAimingDash()
        {

        }
        protected virtual void OnStoppedAimingDash()
        {

        }
        protected virtual void OnStartedCrouching()
        {

        }
        protected virtual void OnStoppedCrouching()
        {

        }
        protected virtual void OnAnimationStarted()
        {

        }
        protected virtual void OnAnimationFinished()
        {

        }
        protected virtual void OnAnimationEnteredActionFrame()
        {
            _hasEnteredActionFrame = true;
        }
        protected virtual void OnDashTargetStruck(DashTarget target)
        {

        }
    }
}