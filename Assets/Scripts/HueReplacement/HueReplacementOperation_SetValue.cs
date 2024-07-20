using UnityEngine;

public class HueReplacementOperation_SetValue
{
    private int _index;
    private Color _value;

    public HueReplacementOperation_SetValue(int index, Color value)
    {
        _index = index;
        _value = value;
    }

    public void Perform(HueReplacement map)
    {
        map.SetValue(_index, _value);
    }
}
