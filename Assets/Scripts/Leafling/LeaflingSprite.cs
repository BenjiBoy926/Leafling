using NaughtyAttributes;
using UnityEngine;

namespace Leafling
{
    public class LeaflingSprite : MonoBehaviour
    {
        [SerializeField]
        private SpriteHueMap _map;
        [SerializeField]
        private int _leftArmColorIndex = 1;
        [SerializeField]
        private int _rightArmColorIndex = 2;

        [Space]
        [SerializeField, ReadOnly]
        private Color _leftArmDefaultColor;
        [SerializeField, ReadOnly]
        private Color _rightArmDefaultColor;

        private void Reset()
        {
            _map = GetComponent<SpriteHueMap>();
        }
        private void Awake()
        {
            _leftArmDefaultColor = _map.GetValue(_leftArmColorIndex);
            _rightArmDefaultColor = _map.GetValue(_rightArmColorIndex);
        }

        public void DesaturateArmColor()
        {
            HueMapOperation_SetMultipleValues setValues = new(
                new HueMapOperation_SetValue(_leftArmColorIndex, DesaturateColor(_leftArmDefaultColor)),
                new HueMapOperation_SetValue(_rightArmColorIndex, DesaturateColor(_rightArmDefaultColor)));
            _map.SetValues(setValues);
        }
        public void ResetArmColor()
        {
            HueMapOperation_SetMultipleValues setValues = new(
                new HueMapOperation_SetValue(_leftArmColorIndex, _leftArmDefaultColor),
                new HueMapOperation_SetValue(_rightArmColorIndex, _rightArmDefaultColor));
            _map.SetValues(setValues);
        }

        private Color DesaturateColor(Color color)
        {
            Color.RGBToHSV(color, out float h, out float s, out float v);
            return Color.HSVToRGB(h, 0, v);
        }
    }
}