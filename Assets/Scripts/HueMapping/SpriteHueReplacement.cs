using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SpriteHueReplacement : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _renderer;
    [SerializeField, FormerlySerializedAs("_map")]
    private HueReplacement _replacements;

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
        return _replacements.GetValue(i);
    }
    public void SetValues(HueMapOperation_SetMultipleValues operation)
    {
        operation.Perform(_replacements);
        RefreshShader();
    }

    [Button]
    private void RefreshShader()
    {
        if (_replacements.Count == 0)
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
            _propertyBlock.SetInt(_mapSizeID, _replacements.Count);
        }
        else
        {
            _renderer.sharedMaterial.SetInt(_mapSizeID, _replacements.Count);
        }
    }
    private void SetMapKeys()
    {
        SetVectorArray(_mapKeysID, _replacements.HsvKeys);
    }
    private void SetMapValues()
    {
        SetVectorArray(_mapValuesID, _replacements.HsvValues);
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
