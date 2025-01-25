using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[ExecuteAlways]
public class SpriteHueReplacement : MonoBehaviour
{
    private Material MaterialInstance
    {
        get
        {
            if (_materialInstance == null)
            {
                _materialInstance = _renderer.material;
            }
            return _materialInstance;
        }
    }
    private MaterialPropertyBlock PropertyBlock => _propertyBlock ??= new MaterialPropertyBlock();

    [SerializeField]
    private Renderer _renderer;
    [SerializeField, FormerlySerializedAs("_map")]
    private HueReplacement _replacements;

    private readonly int _mapSizeID = Shader.PropertyToID("_MapSize");
    private readonly int _mapKeysID = Shader.PropertyToID("_MapKeys");
    private readonly int _mapValuesID = Shader.PropertyToID("_MapValues");
    private Material _materialInstance;
    private MaterialPropertyBlock _propertyBlock;

    private void Awake()
    {
        RefreshShader();
    }
    private void OnDestroy()
    {
        if (_materialInstance != null)
        {
            Destroy(_materialInstance);
        }
    }

    public Color GetValue(int i)
    {
        return _replacements.GetValue(i);
    }
    public void SetValues(HueReplacementOperation_SetMultipleValues operation)
    {
        operation.Perform(_replacements);
        RefreshShader();
    }

    [Button]
    private void RefreshShader()
    {
        if (!IsOperable())
        {
            return;
        }
        if (Application.isPlaying)
        {
            _renderer.GetPropertyBlock(PropertyBlock);
        }
        SetMapSize();
        SetMapKeys();
        SetMapValues();
        if (Application.isPlaying)
        {
            _renderer.SetPropertyBlock(PropertyBlock);
        }
    }
    private void SetMapSize()
    {
        if (Application.isPlaying)
        {
            PropertyBlock.SetInt(_mapSizeID, _replacements.Count);
        }
        else
        {
            MaterialInstance.SetInt(_mapSizeID, _replacements.Count);
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
            PropertyBlock.SetVectorArray(id, array);
        }
        else
        {
            MaterialInstance.SetVectorArray(id, array);
        }
    }

    private void OnValidate()
    {
        RefreshShader();
    }
    private void Reset()
    {
        _renderer = GetComponent<Renderer>();
    }
    private bool IsOperable()
    {
        return _renderer != null && _renderer.sharedMaterial != null && _replacements != null && _replacements.Count > 0;
    }
}
