using UnityEngine;

namespace Leafling
{
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField]
        private Transform _target;
        [SerializeField]
        private float _yOffset;
        [SerializeField]
        private float _zOffset;

        private void Update()
        {
            transform.position = _target.position + CalculateOffset();
        }
        private Vector3 CalculateOffset()
        {
            return new(0, _yOffset, _zOffset);
        }
    }
}