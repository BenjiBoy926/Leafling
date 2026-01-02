using UnityEngine;
using DG.Tweening;

public class DashReticle : MonoBehaviour
{
    [System.Serializable]
    private struct Transition
    {
        public Color Color;
        public float Scale;
        public Ease Ease;

        public void Set(SpriteRenderer renderer, Transform scaleTransform)
        {
            renderer.color = Color;
            scaleTransform.localScale = Scale * Vector3.one;
        }

        public void Perform(SpriteRenderer renderer, Transform scaleTransform, float duration)
        {
            renderer.DOComplete();
            scaleTransform.DOComplete();
            renderer.DOColor(Color, duration);
            scaleTransform.DOScale(Scale, duration).SetEase(Ease);
        }
    }

    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    [SerializeField]
    private Transform _scaleTransform;
    [SerializeField]
    private float _transitionDuration = 0.35f;
    [SerializeField]
    private Transition _defaultTransition;
    [SerializeField]
    private Transition _highlightTransition;
    [SerializeField]
    private Transition _flashTransition;
    [SerializeField]
    private float _lerpStrength = 20;
    private float _currentAngle = 0;
    private float _targetAngle;

    public void ShowAim(Vector2 aim)
    {
        _targetAngle = Mathf.Atan2(aim.y, aim.x) * Mathf.Rad2Deg;
    }

    public void SetHighlight()
    {
        _highlightTransition.Perform(_spriteRenderer, _scaleTransform, _transitionDuration);
    }

    public void ClearHighlight()
    {
        _defaultTransition.Perform(_spriteRenderer, _scaleTransform, _transitionDuration);
    }

    public void Flash()
    {
        _highlightTransition.Set(_spriteRenderer, _scaleTransform);
        _flashTransition.Perform(_spriteRenderer, _scaleTransform, _transitionDuration);
    }

    private void OnEnable()
    {
        _defaultTransition.Set(_spriteRenderer, _scaleTransform);
    }

    private void Update()
    {
        _currentAngle = Mathf.LerpAngle(_currentAngle, _targetAngle, _lerpStrength * Time.deltaTime);
        Vector3 eulers = new(0, 0, _currentAngle);
        transform.localEulerAngles = eulers;
    }
}
