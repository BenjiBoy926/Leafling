using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using NaughtyAttributes;

[ExecuteAlways]
public class SpriteHueReplacement : MonoBehaviour
{
    [SerializeField]
    private Renderer _renderer;
    [SerializeField, FormerlySerializedAs("_map")]
    private HueReplacement _replacements;

    private readonly int _mapSizeID = Shader.PropertyToID("_MapSize");
    private readonly int _mapKeysID = Shader.PropertyToID("_MapKeys");
    private readonly int _mapValuesID = Shader.PropertyToID("_MapValues");
    private Material _materialAsset;
    private Material _materialInstance;

    private void Awake()
    {
        RefreshShader();
    }
    private void OnDestroy()
    {
        if (_materialInstance)
        {
            DestroyMaterialInstance();
        }
        _renderer.sharedMaterial = _materialAsset;
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
        if (!IsOperable()) return;

        if (!_materialInstance)
        {
            CreateMaterialInstance();
        }
        if (!_materialInstance) return;

        SetMapSize();
        SetMapKeys();
        SetMapValues();
    }
    private void SetMapSize()
    {
        _materialInstance.SetInt(_mapSizeID, _replacements.Count);
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
        _materialInstance.SetVectorArray(id, array);
    }

    private void CreateMaterialInstance()
    {
        if (!_renderer.sharedMaterial) return;

        _materialAsset = _renderer.sharedMaterial;
        _materialInstance = Instantiate(_materialAsset);
        _renderer.sharedMaterial = _materialInstance;
    }
    private void DestroyMaterialInstance()
    {
        if (Application.isPlaying)
        {
            Destroy(_materialInstance);
        }
        else
        {
            DestroyImmediate(_materialInstance);
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
        return _renderer != null && _replacements != null && _replacements.Count > 0;
    }
}
