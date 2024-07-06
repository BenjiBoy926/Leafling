using NaughtyAttributes;
using UnityEngine;

public class LeaflingSprite : MonoBehaviour
{
    [SerializeField]
    private SpriteHueMap _map;
    [SerializeField]
    private int _leftArmColorIndex = 1;
    [SerializeField]
    private int _rightArmColorIndex = 2;

    private HueMapOperation_SetMultipleValues _setDesaturatedColors;
    private HueMapOperation_SetMultipleValues _setDefaultColors;

    private void Reset()
    {
        _map = GetComponent<SpriteHueMap>();
    }
    private void Awake()
    {
        Color leftArmDefaultColor = _map.GetValue(_leftArmColorIndex);
        Color rightArmDefaultColor = _map.GetValue(_rightArmColorIndex);
        _setDesaturatedColors = new(
            new HueMapOperation_SetValue(_leftArmColorIndex, DesaturateColor(leftArmDefaultColor)),
            new HueMapOperation_SetValue(_rightArmColorIndex, DesaturateColor(rightArmDefaultColor)));
        _setDefaultColors = new(
            new HueMapOperation_SetValue(_leftArmColorIndex, leftArmDefaultColor),
            new HueMapOperation_SetValue(_rightArmColorIndex, rightArmDefaultColor));
    }

    public void DesaturateArmColor()
    {
        _map.SetValues(_setDesaturatedColors);
    }
    public void ResetArmColor()
    {
        _map.SetValues(_setDefaultColors);
    }

    private Color DesaturateColor(Color color)
    {
        Color.RGBToHSV(color, out float h, out _, out float v);
        return Color.HSVToRGB(h, 0, v);
    }
}
