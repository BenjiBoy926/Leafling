using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Leafling
{
    public class LeaflingState
    {
        protected Leafling Leafling => _leafling;
        protected float TimeSinceStateStart => Time.time - _timeOfStateStart;

        private Leafling _leafling;
        private float _timeOfStateStart;

        public LeaflingState(Leafling leafling)
        {
            _leafling = leafling;
        }
        public virtual void Enter()
        {
            _timeOfStateStart = Time.time;
            Leafling.HorizontalDirectionChanged += OnHorizontalDirectionChanged;
            Leafling.StartedJumping += OnStartedJumping;
            Leafling.StoppedJumping += OnStoppedJumping;
            Leafling.StartedAimingDash += OnStartedAimingDash;
            Leafling.StoppedAimingDash += OnStoppedAimingDash;
            Leafling.StartedCrouching += OnStartedCrouching;
            Leafling.StoppedCrouching += OnStoppedCrouching;
            Leafling.AnimationStarted += OnAnimationStarted;
            Leafling.AnimationFinished += OnAnimationFinished;
        }
        public virtual void Exit()
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
        }
        public virtual void Update(float dt)
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
    }
}