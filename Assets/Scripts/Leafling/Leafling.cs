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

        public int HorizontalDirection => _inputs.HorizontalDirection;
        public bool IsJumping => _inputs.IsJumping;
        public bool IsAimingDash => _inputs.IsAimingDash;
        public Vector2 DashAim => _inputs.DashAim;
        public float DashAimX => _inputs.DashAimX;
        public float DashAimY => _inputs.DashAimY;
        public bool IsCrouching => _inputs.IsCrouching;

        public int FacingDirection => FlipXToDirection(_animator.FlipX);
        public bool CurrentFlipX => _animator.FlipX;
        public float CurrentFrameProgress => _animator.CurrentFrameProgress;
        public float ProgressAfterFirstActionFrame => _animator.ProgressAfterFirstActionFrame;
        public bool IsCurrentFrameFirstFrame => _animator.IsCurrentFrameFirstFrame;
        public bool IsTransitioningOnCurrentFrame => _animator.IsTransitioningOnCurrentFrame();
        public bool IsNextFrameActionFrame => _animator.IsNextFrameActionFrame;
        public bool IsPreviousFrameActionFrame => _animator.IsPreviousFrameActionFrame;
        public bool IsCurrentFrameActionFrame => _animator.IsCurrentFrameActionFrame;

        public SpriteAnimation Idle => _idle;
        public SpriteAnimation Run => _run;
        public SpriteAnimation Jump => _jump;
        public SpriteAnimation Backflip => _backflip;
        public SpriteAnimation FreeFallForward => _freeFallForward;
        public SpriteAnimation FreeFallBack => _freeFallBack;
        public SpriteAnimation FreeFallStraight => _freeFallStraight; 
        public SpriteAnimation Flutter => _flutter;
        public SpriteAnimation Squat => _squat;
        public SpriteAnimation MidairDashAim => _midairDashAim;
        public SpriteAnimation WallPerch => _wallPerch;
        public SpriteAnimation CeilingPerch => _ceilingPerch;
        public SpriteAnimation Dash => _dash;
        public SpriteAnimation Drop => _drop;

        public float BaseRunSpeed => _baseRunSpeed;
        public float LeapMaxSpeed => _baseRunSpeed + _leapAdditionalSpeed;
        public AnimationCurve RunAccelerationCurve => _runAccelerationCurve;
        public float MaxJumpSpeed => _maxJumpSpeed;
        public float MaxJumpTime => _maxJumpTime;
        public float MaxDashSpeed => _maxDashSpeed;
        public DirectionalAirControl JumpAirControl => _jumpAirControl;
        public DirectionalAirControl FreeFallAirControl => _freeFallAirControl;
        public DirectionalAirControl FlutterAirControl => _flutterAirControl;
        public DirectionalAirControl DropAirControl => _dropAirControl;
        public float AimingDashGravityScale => _aimingDashGravityScale;

        [Header("Parts")]
        [SerializeField]
        private LeaflingStateMachine _stateMachine;
        [SerializeField]
        private Rigidbody2D _physicsBody;
        [SerializeField]
        private SpriteAnimator _animator;
        [SerializeField]
        private CardinalContacts _contacts;
        [SerializeField]
        private LeaflingInputs _inputs;

        [Header("Animations")]
        [SerializeField]
        private SpriteAnimation _idle;
        [SerializeField]
        private SpriteAnimation _run;
        [SerializeField]
        private SpriteAnimation _jump;
        [SerializeField]
        private SpriteAnimation _backflip;
        [SerializeField]
        private SpriteAnimation _freeFallForward;
        [SerializeField]
        private SpriteAnimation _freeFallBack;
        [SerializeField]
        private SpriteAnimation _freeFallStraight;
        [SerializeField]
        private SpriteAnimation _flutter;
        [SerializeField]
        private SpriteAnimation _drop;
        [SerializeField]
        private SpriteAnimation _squat;
        [SerializeField]
        private SpriteAnimation _midairDashAim;
        [SerializeField]
        private SpriteAnimation _wallPerch;
        [SerializeField]
        private SpriteAnimation _ceilingPerch;
        [SerializeField, FormerlySerializedAs("_sideDashAnimation")]
        private SpriteAnimation _dash;

        [Header("Running")]
        [SerializeField]
        private float _baseRunSpeed = 1;
        [SerializeField]
        private float _leapAdditionalSpeed = 1;
        [SerializeField, FormerlySerializedAs("_accelerationCurve")]
        private AnimationCurve _runAccelerationCurve;

        [Header("Jumping")]
        [SerializeField]
        private float _maxJumpSpeed = 5;
        [SerializeField]
        private AnimationCurve _jumpSpeedCurve;
        [SerializeField]
        private float _maxJumpTime = 1;
        [SerializeField]
        private DirectionalAirControl _jumpAirControl;
        [SerializeField]
        private DirectionalAirControl _freeFallAirControl;
        [SerializeField]
        private DirectionalAirControl _flutterAirControl;
        [SerializeField]
        private DirectionalAirControl _dropAirControl;

        [Header("Dashing")]
        [SerializeField]
        private float _aimingDashGravityScale = 0.1f;
        [SerializeField]
        private float _maxDashSpeed = 40;
        [SerializeField]
        private AnimationCurve _dashSpeedCurve;
        private float _defaultGravityScale;
        private Quaternion _defaultSpriteRotation;

        private void Awake()
        {
            _defaultGravityScale = _physicsBody.gravityScale;
            _defaultSpriteRotation = _animator.transform.localRotation;
            SetState(new LeaflingFreeFallState(this, FreeFallEntry.Normal));
        }
        private void OnEnable()
        {
            _inputs.HorizontalDirectionChanged += OnHorizontalDirectionChanged;
            _inputs.StartedJumping += OnStartedJumping;
            _inputs.StoppedJumping += OnStoppedJumping;
            _inputs.StartedAimingDash += OnStartedAimingDash;
            _inputs.StoppedAimingDash += OnStoppedAimingDash;
            _inputs.StartedCrouching += OnStartedCrouching;
            _inputs.StoppedCrouching += OnStoppedCrouching;
            _animator.StartedAnimation += OnAnimationStarted;
            _animator.FinishedAnimation += OnAnimationFinished;
        }
        private void OnDisable()
        {
            _inputs.HorizontalDirectionChanged -= OnHorizontalDirectionChanged;
            _inputs.StartedJumping -= OnStartedJumping;
            _inputs.StoppedJumping -= OnStoppedJumping;
            _inputs.StartedAimingDash -= OnStartedAimingDash;
            _inputs.StoppedAimingDash -= OnStoppedAimingDash;
            _inputs.StartedCrouching -= OnStartedCrouching;
            _inputs.StoppedCrouching -= OnStoppedCrouching;
            _animator.StartedAnimation -= OnAnimationStarted;
            _animator.FinishedAnimation -= OnAnimationFinished;
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

        public void SetState(LeaflingState state)
        {
            _stateMachine.SetState(state);
        }

        public void SetHorizontalVelocity(float velocity)
        {
            _physicsBody.SetVelocity(velocity, Dimension.X);
        }
        public void SetVerticalVelocity(float velocity)
        {
            _physicsBody.SetVelocity(velocity, Dimension.Y);
        }
        public void SetVelocity(Vector2 velocity)
        {
            _physicsBody.velocity = velocity;
        }

        public float EvaluateJumpSpeedCurve(float t)
        {
            return _jumpSpeedCurve.Evaluate(t);
        }
        public float EvaluateDashSpeedCurve(float t)
        {
            return _dashSpeedCurve.Evaluate(t);
        }
        public void ApplyAirControl(DirectionalAirControl control)
        {
            control.ApplyTo(_physicsBody, HorizontalDirection, FacingDirection);
        }
        public void SetGravityScale(float scale)
        {
            _physicsBody.gravityScale = scale;
        }
        public void ResetGravityScale()
        {
            _physicsBody.gravityScale = _defaultGravityScale;
        }
        public void SetSpriteUp(Vector2 up)
        {
            _animator.transform.up = up;
        }
        public void SetSpriteRight(Vector2 right)
        {
            _animator.transform.right = right;
        }
        public void ResetSpriteRotation()
        {
            _animator.transform.localRotation = _defaultSpriteRotation;
        }

        public bool IsTouching(CardinalDirection direction)
        {
            return _contacts.IsTouching(direction);
        }
        public bool IsTouchingAnything()
        {
            return _contacts.IsTouchingAnything();
        }
        public IEnumerable<Vector2> GetContactNormals()
        {
            return _contacts.GetContactNormals();
        }

        public void SetTransition(SpriteAnimationTransition transition)
        {
            _animator.SetTransition(transition);
        }
        public void SetAnimation(SpriteAnimation animation)
        {
            _animator.SetAnimation(animation);
        }
        public void FaceTowards(float direction)
        {
            _animator.SetFlipX(DirectionToFlipX(direction));
        }

        public bool IsAnimating(SpriteAnimation animation)
        {
            return _animator.IsAnimating(animation);
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