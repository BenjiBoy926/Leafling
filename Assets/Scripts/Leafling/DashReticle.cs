using UnityEngine;

public class DashReticle : MonoBehaviour
{
    [SerializeField]
    private float _lerpStrength = 20;
    private float _currentAngle = 0;
    private float _targetAngle;

    public void ShowAim(Vector2 aim)
    {
        _targetAngle = Mathf.Atan2(aim.y, aim.x) * Mathf.Rad2Deg;
    }

    private void Update()
    {
        _currentAngle = Mathf.LerpAngle(_currentAngle, _targetAngle, _lerpStrength * Time.deltaTime);
        Vector3 eulers = new(0, 0, _currentAngle);
        transform.localEulerAngles = eulers;
    }
}
