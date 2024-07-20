using System;
using UnityEngine;

[Serializable]
public class HueReplacementItem
{
    private const string BackingFieldFormat = "<{0}>k__BackingField";
    public static string KeyBackingFieldName => string.Format(BackingFieldFormat, nameof(Key));
    public static string ValueBackingFieldName => string.Format(BackingFieldFormat, nameof(Value));

    public Vector4 HsvKey => ToHsv(Key);
    public Vector4 HsvValue => ToHsv(Value);

    [field: SerializeField]
    public Color Key { get; private set; } = Color.white;
    [field: SerializeField]
    public Color Value { get; set; } = Color.white;

    public static Vector4 ToHsv(Color color)
    {
        Color.RGBToHSV(color, out float h, out float s, out float v);
        return new(h, s, v, color.a);
    }
}
