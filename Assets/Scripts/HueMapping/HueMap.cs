using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class HueMap
{
    public int Count => _keyValuePairs.Length;
    public List<Vector4> HsvKeys => _keyValuePairs.Select(HsvKey).ToList();
    public List<Vector4> HsvValues => _keyValuePairs.Select(HsvValue).ToList();

    [SerializeField]
    private ColorPair[] _keyValuePairs;

    public Color GetValue(int i)
    {
        return _keyValuePairs[i].Value;
    }
    public void SetValue(int i, Color value)
    {
        _keyValuePairs[i].Value = value;
    }
    private Vector4 HsvKey(ColorPair pair)
    {
        return pair.HsvKey;
    }
    private Vector4 HsvValue(ColorPair pair)
    {
        return pair.HsvValue;
    }
}
