using UnityEngine;
using DG.Tweening;

public class DashReticle : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    [SerializeField]
    private Transform _scaleTransform;
    [SerializeField]
    private Color _defaultColor;
    [SerializeField]
    private Color _highlightColor;
    [SerializeField]
    private float _defaultScale = 1;
    [SerializeField]
    private float _highlightScale = 1.2f;
    [SerializeField]
    private float _highlightTransitionDuration = 0.35f;
    [SerializeField]
    private Ease _highlightEase = Ease.OutBack;
    [SerializeField]
    private Ease _defaultEase = Ease.OutCubic;
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
        _scaleTransform.DOKill();
        _spriteRenderer.DOKill();
        _scaleTransform.DOScale(_highlightScale, _highlightTransitionDuration).SetEase(_highlightEase);
        _spriteRenderer.DOColor(_highlightColor, _highlightTransitionDuration);
    }

    public void ClearHighlight()
    {
        _scaleTransform.DOKill();
        _spriteRenderer.DOKill();
        _scaleTransform.DOScale(_defaultScale, _highlightTransitionDuration).SetEase(_defaultEase);
        _spriteRenderer.DOColor(_defaultColor, _highlightTransitionDuration);
    }

    private void Update()
    {
        _currentAngle = Mathf.LerpAngle(_currentAngle, _targetAngle, _lerpStrength * Time.deltaTime);
        Vector3 eulers = new(0, 0, _currentAngle);
        transform.localEulerAngles = eulers;
    }
}
