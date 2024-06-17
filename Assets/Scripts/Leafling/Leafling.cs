using Core;
using NaughtyAttributes;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Leafling
{
    public class Leafling : MonoBehaviour
    {
        public event Action HorizontalDirectionChanged = delegate { };
        public event Action StartedJumping = delegate { };
        public event Action StoppedJumping = delegate { };
        public event Action StartedAimingDash = delegate { };
        public event Action StoppedAimingDash = delegate { };
        public event Action StartedCrouching = delegate { };
        public event Action StoppedCrouching = delegate { };
        public event Action AnimationStarted = delegate { };
        public event Action AnimationFinished = delegate { };
        public event Action AnimationEnteredActionFrame = delegate { };

        public int HorizontalDirection => Inputs.HorizontalDirection;
        public bool IsJumping => Inputs.IsJumping;
        public bool IsAimingDash => Inputs.IsAimingDash;
        public Vector2 DashAim => Inputs.DashAim;
        public bool IsCrouching => Inputs.IsCrouching;

        public int FacingDirection => FlipXToDirection(Animator.FlipX);
        public bool CurrentFlipX => Animator.FlipX;
        public float CurrentFrameProgress => Animator.CurrentFrameProgress;
        public float ProgressAfterFirstActionFrame => Animator.ProgressAfterFirstActionFrame;
        public float ProgressOfFirstActionFrame => Animator.ProgressOfFirstActionFrame;
        public bool IsCurrentFrameFirstFrame => Animator.IsCurrentFrameFirstFrame;
        public bool IsTransitioningOnCurrentFrame => Animator.IsTransitioningOnCurrentFrame();
        public bool IsNextFrameActionFrame => Animator.IsNextFrameActionFrame;
        public bool IsPreviousFrameActionFrame => Animator.IsPreviousFrameActionFrame;
        public bool IsCurrentFrameActionFrame => Animator.IsCurrentFrameActionFrame;

        public float LeapMaxSpeed => BaseRunSpeed + LeapAdditionalSpeed;
        public float LongJumpTopSpeed => JumpAirControl.ForwardTopSpeed;

        [field: Header("Parts")]
        [field: SerializeField]
        private LeaflingStateMachine StateMachine { get; set; }
        [field: SerializeField]
        private Rigidbody2D PhysicsBody { get; set; }
        [field: SerializeField]
        private SpriteAnimator Animator { get; set; }
        [field: SerializeField]
        private CardinalContacts Contacts { get; set; }
        [field: SerializeField]
        private LeaflingInputs Inputs { get; set; }

        [field: Header("Animation")]
        [field: SerializeField]
        public SpriteAnimation Idle { get; private set; }
        [field: SerializeField]
        public SpriteAnimation Run { get; private set; }
        [field: SerializeField]
        public SpriteAnimation Slide { get; private set; }
        [field: SerializeField]
        public SpriteAnimation LongJump { get; private set; }
        [field: SerializeField]
        public SpriteAnimation Jump { get; private set; }
        [field: SerializeField]
        public SpriteAnimation CrouchJump { get; private set; }
        [field: SerializeField]
        public SpriteAnimation Backflip { get; private set; }
        [field: SerializeField]
        public SpriteAnimation Frontflip { get; private set; }
        [field: SerializeField]
        public SpriteAnimation FreeFallForward { get; private set; }
        [field: SerializeField]
        public SpriteAnimation FreeFallBack { get; private set; }
        [field: SerializeField]
        public SpriteAnimation FreeFallStraight { get; private set; }
        [field: SerializeField]
        public SpriteAnimation Flutter { get; private set; }
        [field: SerializeField]
        public SpriteAnimation Squat { get; private set; }
        [field: SerializeField]
        public SpriteAnimation MidairDashAim { get; private set; }
        [field: SerializeField]
        public SpriteAnimation WallPerch { get; private set; }
        [field: SerializeField]
        public SpriteAnimation CeilingPerch { get; private set; }
        [field: SerializeField]
        public SpriteAnimation Dash { get; private set; }
        [field: SerializeField]
        public SpriteAnimation Drop { get; private set; }
        [field: SerializeField]
        public SpriteAnimation WallJump { get; private set; }

        [field: Space]
        [field: SerializeField]
        public float RunningTransitionScale { get; private set; } = 0.5f;
        [field: SerializeField]
        public float SlideTransitionScale { get; private set; } = 0.25f;
        [field: SerializeField]
        public float JumpTransitionScale { get; private set; } = 0.1f;
        [field: SerializeField]
        public float FlutterTransitionScale { get; private set; } = 0.5f;
        [field: SerializeField]
        public float DropTransitionScale { get; private set; } = 0.5f;
        [field: SerializeField]
        public float AimingDashTransitionScale { get; private set; } = 0.5f;
        [field: SerializeField]
        public float WallSlideTransitionScale { get; private set; } = 0.5f;

        [field: Header("Running")]
        [field: SerializeField]
        public float BaseRunSpeed { get; private set; } = 5;
        [field: SerializeField]
        private float LeapAdditionalSpeed { get; set; } = 15;
        [field: SerializeField]
        public AnimationCurve RunAccelerationCurve { get; private set; }
        [field: SerializeField]
        public float MaxSlideSpeed { get; private set; } = 25;
        [field: SerializeField]
        private AnimationCurve SlideSpeedCurve { get; set; }
        [field: SerializeField]
        public DirectionalAirControl LongJumpAirControl { get; private set; }
        [field: SerializeField]
        public float LongJumpGravityScale { get; private set; } = 0.1f;

        [field: Header("Jumping")]
        [field: SerializeField]
        public float MaxJumpSpeed { get; private set; } = 20;
        [field: SerializeField]
        private AnimationCurve JumpSpeedCurve { get; set; }
        [field: SerializeField]
        public float MaxJumpTime { get; private set; } = 0.5f;
        [field: SerializeField]
        public float DropSpeed { get; private set; } = 30;
        [field: SerializeField]
        public float DropCancelSpeed { get; private set; } = 10;
        [field: SerializeField]
        public DirectionalAirControl JumpAirControl { get; private set; }
        [field: SerializeField]
        public DirectionalAirControl FreeFallAirControl { get; private set; }
        [field: SerializeField]
        public DirectionalAirControl FlutterAirControl { get; private set; }
        [field: SerializeField]
        public DirectionalAirControl DropAirControl { get; private set; }
        [field: SerializeField]
        public float CrouchJumpSpeed { get; private set; } = 35;
        [field: SerializeField]
        public DirectionalAirControl CrouchJumpAirControl { get; private set; }
        [field: SerializeField]
        public float WallSlideGravityScale { get; private set; } = 0.25f;
        [field: SerializeField]
        public float MaxWallJumpTime { get; private set; } = 0.5f;
        [field: SerializeField]
        public float WallJumpSpeed { get; private set; } = 30;
        [field: SerializeField]
        public float WallJumpExitHop { get; private set; } = 10;

        [field: Header("Dashing")]
        [field: SerializeField]
        public float AimingDashGravityScale { get; private set; } = 0.25f;
        [field: SerializeField]
        public float MaxDashSpeed { get; private set; } = 30;
        [field: SerializeField]
        private AnimationCurve DashSpeedCurve { get; set; }
        [field: SerializeField]
        public float DashCancelSpeed { get; private set; } = 30;

        private float _defaultGravityScale;
        private Quaternion _defaultSpriteRotation;

        private void Awake()
        {
            _defaultGravityScale = PhysicsBody.gravityScale;
            _defaultSpriteRotation = Animator.transform.localRotation;
            SetState(new LeaflingState_FreeFall(this, FreeFallEntry.Normal));
        }
        private void OnEnable()
        {
            Inputs.HorizontalDirectionChanged += OnHorizontalDirectionChanged;
            Inputs.StartedJumping += OnStartedJumping;
            Inputs.StoppedJumping += OnStoppedJumping;
            Inputs.StartedAimingDash += OnStartedAimingDash;
            Inputs.StoppedAimingDash += OnStoppedAimingDash;
            Inputs.StartedCrouching += OnStartedCrouching;
            Inputs.StoppedCrouching += OnStoppedCrouching;
            Animator.StartedAnimation += OnAnimationStarted;
            Animator.FinishedAnimation += OnAnimationFinished;
            Animator.ActionFrameEntered += OnAnimationEnteredActionFrame;
        }
        private void OnDisable()
        {
            Inputs.HorizontalDirectionChanged -= OnHorizontalDirectionChanged;
            Inputs.StartedJumping -= OnStartedJumping;
            Inputs.StoppedJumping -= OnStoppedJumping;
            Inputs.StartedAimingDash -= OnStartedAimingDash;
            Inputs.StoppedAimingDash -= OnStoppedAimingDash;
            Inputs.StartedCrouching -= OnStartedCrouching;
            Inputs.StoppedCrouching -= OnStoppedCrouching;
            Animator.StartedAnimation -= OnAnimationStarted;
            Animator.FinishedAnimation -= OnAnimationFinished;
            Animator.ActionFrameEntered -= OnAnimationEnteredActionFrame;
        }
        private void OnHorizontalDirectionChanged()
        {
            HorizontalDirectionChanged();
        }
        private void OnStartedJumping()
        {
            StartedJumping();
        }
        private void OnStoppedJumping()
        {
            StoppedJumping();
        }
        private void OnStartedAimingDash()
        {
            StartedAimingDash();
        }
        private void OnStoppedAimingDash()
        {
            StoppedAimingDash();
        }
        private void OnStartedCrouching()
        {
            StartedCrouching();
        }
        private void OnStoppedCrouching()
        {
            StoppedCrouching();
        }
        private void OnAnimationStarted()
        {
            AnimationStarted();
        }
        private void OnAnimationFinished()
        {
            AnimationFinished();
        }
        private void OnAnimationEnteredActionFrame()
        {
            AnimationEnteredActionFrame();
        }

        public void SetState(LeaflingState state)
        {
            StateMachine.SetState(state);
        }

        public void SetHorizontalVelocity(float velocity)
        {
            PhysicsBody.SetVelocity(velocity, Dimension.X);
        }
        public void SetVerticalVelocity(float velocity)
        {
            PhysicsBody.SetVelocity(velocity, Dimension.Y);
        }
        public void SetVelocity(Vector2 velocity)
        {
            PhysicsBody.velocity = velocity;
        }

        public float EvaluateJumpSpeedCurve(float t)
        {
            return JumpSpeedCurve.Evaluate(t);
        }
        public float EvaluateDashSpeedCurve(float t)
        {
            return DashSpeedCurve.Evaluate(t);
        }
        public float EvaluateSlideSpeedCurve(float t)
        {
            return SlideSpeedCurve.Evaluate(t);
        }
        public void ApplyAirControl(DirectionalAirControl control)
        {
            control.ApplyTo(PhysicsBody, HorizontalDirection, FacingDirection);
        }
        public void SetGravityScale(float scale)
        {
            PhysicsBody.gravityScale = scale;
        }
        public void ResetGravityScale()
        {
            PhysicsBody.gravityScale = _defaultGravityScale;
        }
        public void SetSpriteUp(Vector2 up)
        {
            Animator.transform.up = up;
        }
        public void SetSpriteRight(Vector2 right)
        {
            Animator.transform.right = right;
        }
        public void ResetSpriteRotation()
        {
            Animator.transform.localRotation = _defaultSpriteRotation;
        }

        public bool IsTouching(CardinalDirection direction)
        {
            return Contacts.IsTouching(direction);
        }
        public bool IsTouchingAnything()
        {
            return Contacts.IsTouchingAnything();
        }
        public IEnumerable<Vector2> GetContactNormals()
        {
            return Contacts.GetContactNormals();
        }

        public void SetTransition(SpriteAnimationTransition transition)
        {
            Animator.SetTransition(transition);
        }
        public void SetAnimation(SpriteAnimation animation)
        {
            Animator.SetAnimation(animation);
        }
        public void FaceTowards(float direction)
        {
            Animator.SetFlipX(DirectionToFlipX(direction));
        }

        public bool IsAnimating(SpriteAnimation animation)
        {
            return Animator.IsAnimating(animation);
        }

        public static bool DirectionToFlipX(float direction)
        {
            return direction < 0;
        }
        public static int FlipXToDirection(bool flip)
        {
            return flip ? -1 : 1;
        }
    }
}