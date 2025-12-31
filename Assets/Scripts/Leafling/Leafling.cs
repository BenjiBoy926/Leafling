using Core;
using System;
using System.Collections.Generic;
using UnityEngine;

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
    public event DashTargeter.TargetStrikeHandler DashTargetTouched = delegate { };

    public int HorizontalDirection => Inputs.HorizontalDirection;
    public float HorizontalVelocity => PhysicsBody.velocity.x;
    public bool IsJumping => Inputs.IsJumping;
    public bool IsAimingDash => Inputs.IsAimingDash;
    public Vector2 DashAim => _clampedDashAim;
    public bool IsCrouching => Inputs.IsCrouching;

    public int FacingDirection => FlipXToDirection(Animator.FlipX);
    public bool CurrentFlipX => Animator.FlipX;
    public float CurrentFrameProgress => Animator.CurrentFrameProgress;
    public float CurrentAnimationProgress => Animator.CurrentAnimationProgress;
    public float ProgressAfterFirstActionFrame => Animator.ProgressAfterFirstActionFrame;
    public float ProgressOfFirstActionFrame => Animator.ProgressOfFirstActionFrame;
    public bool IsCurrentFrameFirstFrame => Animator.IsCurrentFrameFirstFrame;
    public bool IsTransitioningOnCurrentFrame => Animator.IsTransitioningOnCurrentFrame();
    public bool IsNextFrameActionFrame => Animator.IsNextFrameActionFrame;
    public bool IsPreviousFrameActionFrame => Animator.IsPreviousFrameActionFrame;
    public bool IsCurrentFrameActionFrame => Animator.IsCurrentFrameActionFrame;
    public float VerticalVelocity => PhysicsBody.velocity.y;

    [field: SerializeField]
    private LeaflingStateMachine StateMachine { get; set; }
    [field: SerializeField]
    public Rigidbody2D PhysicsBody { get; private set; }
    [field: SerializeField]
    private SpriteAnimator Animator { get; set; }
    [field: SerializeField]
    private CardinalContacts Contacts { get; set; }
    [field: SerializeField]
    private LeaflingInputs Inputs { get; set; }
    [field: SerializeField]
    private LeaflingSprite Sprite { get; set; }
    [field: SerializeField]
    public DashTargeter DashTargeter { get; private set; }
    [field: SerializeField]
    private DashReticle DashReticle { get; set; }

    [field: Space]
    [field: SerializeField]
    public SpriteAnimation Squat { get; private set; }
    [field: SerializeField]
    public SpriteAnimation MidairDashAim { get; private set; }
    [field: SerializeField]
    public SpriteAnimation WallPerch { get; private set; }
    [field: SerializeField]
    public SpriteAnimation CeilingPerch { get; private set; }
    [field: SerializeField]
    public bool IsAbleToDash { get; private set; } = true;

    private float _defaultGravityScale;
    private Quaternion _defaultSpriteRotation;
    private Vector2 _clampedDashAim;
    private bool _isControllingReticle = true;

    private void Awake()
    {
        _defaultGravityScale = PhysicsBody.gravityScale;
        _defaultSpriteRotation = Animator.transform.localRotation;
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
        DashTargeter.TouchedTarget += OnDashTargetTouched;
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
        DashTargeter.TouchedTarget -= OnDashTargetTouched;
    }
    private void FixedUpdate()
    {
        _clampedDashAim = LeaflingStateTool_Dash.ClampDashAim(this, Inputs.DashAim);
        if (_isControllingReticle)
        {
            ShowAim(_clampedDashAim);
        }
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
    private void OnDashTargetTouched(DashTarget target)
    {
        DashTargetTouched(target);
    }

    public void SendSignal(ILeaflingSignal signal)
    {
        StateMachine.SendSignal(signal);
    }

    public void SetPosition(Vector2 position)
    {
        PhysicsBody.position = position;
    }
    public Vector2 GetPosition()
    {
        return PhysicsBody.position;
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
    public void AddForce(Vector2 force, ForceMode2D mode)
    {
        PhysicsBody.AddForce(force, mode);
    }

    public void ApplyPhysicsControl(PhysicsControl control)
    {
        control.ApplyTo(PhysicsBody, HorizontalDirection);
    }
    public void ApplyPhysicsControl(DirectionalPhysicsControl control)
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
    public PlatformEffector2D GetCurrentPlatform()
    {
        Contact contact = Contacts.GetContact(CardinalDirection.Down);
        bool contactSamePlatform = ContactSamePlatform(contact.Min, contact.Max, out PlatformEffector2D platform);
        return contactSamePlatform ? platform : null;
    }
    public void IgnoreCollision(Collider2D collider)
    {
        Physics2D.IgnoreCollision(Contacts.Collider, collider);
    }
    public void RestoreCollision(Collider2D collider)
    {
        Physics2D.IgnoreCollision(Contacts.Collider, collider, false);
    }
    public void SetColliderY(float y)
    {
        Collider2D collider = Contacts.Collider;
        Vector2 offset = collider.offset;
        offset.y = y;
        collider.offset = offset;
    }

    public void TakeControlOfReticle()
    {
        _isControllingReticle = false;
    }
    public void ReleaseControlOfReticle()
    {
        _isControllingReticle = true;
    }
    public void ShowAim(Vector2 aim)
    {
        DashReticle.ShowAim(aim);
    }
    public void SetDashReticleHighlight()
    {
        DashReticle.SetHighlight();
    }
    public void ClearDashReticleHighlight()
    {
        DashReticle.ClearHighlight();
    }
    public void FlashDashReticle()
    {
        DashReticle.Flash();
    }

    private bool ContactSamePlatform(RaycastHit2D min, RaycastHit2D max, out PlatformEffector2D platform)
    {
        bool minHitPlatform = TryGetPlatform(min, out platform);
        bool maxHitPlatform = TryGetPlatform(max, out PlatformEffector2D otherPlatform);
        return minHitPlatform && maxHitPlatform && platform == otherPlatform;
    }
    private bool TryGetPlatform(RaycastHit2D hit, out PlatformEffector2D platform)
    {
        platform = null;
        return hit.collider && hit.collider.TryGetComponent(out platform);
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

    public void MakeUnableToDash()
    {
        IsAbleToDash = false;
        DesaturateArmColor();
    }
    public void RestoreAbilityToDash()
    {
        IsAbleToDash = true;
        ResetArmColor();
    }
    public void DesaturateArmColor()
    {
        Sprite.DesaturateArmColor();
    }
    public void ResetArmColor()
    {
        Sprite.ResetArmColor();
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
