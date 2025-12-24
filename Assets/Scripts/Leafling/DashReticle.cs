using UnityEngine;

public class DashReticle : MonoBehaviour
{
    [SerializeField]
    private float _radiansPerSecond = 2 * Mathf.PI;
    private float _currentAngle = 0;
    private float _targetAngle;

    public void ShowAim(Vector2 aim)
    {
        _targetAngle = Mathf.Atan2(aim.y, aim.x) * Mathf.Rad2Deg;
    }

    private void Update()
    {
        _currentAngle = Mathf.Lerp(_currentAngle, _targetAngle, _radiansPerSecond * Time.deltaTime);
        Vector3 eulers = new(0, 0, _currentAngle);
        transform.localEulerAngles = eulers;
    }
}
