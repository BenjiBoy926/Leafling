using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

public class SpriteHueMap : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _renderer;
    [SerializeField]
    private HueMap _map;

    private readonly int _mapSizeID = Shader.PropertyToID("_MapSize");
    private readonly int _mapKeysID = Shader.PropertyToID("_MapKeys");
    private readonly int _mapValuesID = Shader.PropertyToID("_MapValues");
    private MaterialPropertyBlock _propertyBlock;

    private void OnValidate()
    {
        RefreshShader();
    }
    private void Reset()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }
    private void Awake()
    {
        RefreshShader();
    }

    public Color GetValue(int i)
    {
        return _map.GetValue(i);
    }
    public void SetValues(HueMapOperation_SetMultipleValues operation)
    {
        operation.Perform(_map);
        RefreshShader();
    }

    [Button]
    private void RefreshShader()
    {
        if (_map.Count == 0)
        {
            return;
        }
        if (Application.isPlaying)
        {
            LazyLoadPropertyBlock();
            _renderer.GetPropertyBlock(_propertyBlock);
        }
        SetMapSize();
        SetMapKeys();
        SetMapValues();
        if (Application.isPlaying)
        {
            _renderer.SetPropertyBlock(_propertyBlock);
        }
    }
    private void LazyLoadPropertyBlock()
    {
        if (_propertyBlock == null)
        {
            _propertyBlock = new();
        }
    }
    private void SetMapSize()
    {
        if (Application.isPlaying)
        {
            _propertyBlock.SetInt(_mapSizeID, _map.Count);
        }
        else
        {
            _renderer.sharedMaterial.SetInt(_mapSizeID, _map.Count);
        }
    }
    private void SetMapKeys()
    {
        SetVectorArray(_mapKeysID, _map.Keys);
    }
    private void SetMapValues()
    {
        SetVectorArray(_mapValuesID, _map.Values);
    }
    private void SetVectorArray(int id, List<Vector4> array)
    {
        if (Application.isPlaying)
        {
            _propertyBlock.SetVectorArray(id, array);
        }
        else
        {
            _renderer.sharedMaterial.SetVectorArray(id, array);
        }
    }
}
