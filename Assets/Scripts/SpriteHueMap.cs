using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

namespace Leafling
{
    public class SpriteHueMap : MonoBehaviour
    {
        private int MapSize => Mathf.Min(_keys.Length, _values.Length);

        [SerializeField]
        private SpriteRenderer _renderer;
        [SerializeField]
        private Color[] _keys;
        [SerializeField]
        private Color[] _values;

        [Space]
        [SerializeField, ReadOnly]
        private List<Vector4> _keysAsVectors = new();
        [SerializeField, ReadOnly]
        private List<Vector4> _valuesAsVectors = new();

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
            _propertyBlock = new MaterialPropertyBlock();
            RefreshShader();
        }

        [Button]
        private void RefreshShader()
        {
            LoadColorsAsVectors();
            if (Application.isPlaying)
            {
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
        private void LoadColorsAsVectors()
        {
            LoadColorsAsVectors(_keys, _keysAsVectors);
            LoadColorsAsVectors(_values, _valuesAsVectors);
        }
        private void LoadColorsAsVectors(Color[] source, List<Vector4> destination)
        {
            while (destination.Count > source.Length)
            {
                destination.RemoveAt(destination.Count - 1);
            }
            while (destination.Count < source.Length)
            {
                destination.Add(new());
            }
            for (int i = 0; i < source.Length; i++)
            {
                destination[i] = source[i];
            }
        }
        private void SetMapSize()
        {
            if (Application.isPlaying)
            {
                _propertyBlock.SetInt(_mapSizeID, MapSize);
            }
            else
            {
                _renderer.sharedMaterial.SetInt(_mapSizeID, MapSize);
            }
        }
        private void SetMapKeys()
        {
            SetVectorArray(_mapKeysID, _keysAsVectors);
        }
        private void SetMapValues()
        {
            SetVectorArray(_mapValuesID, _valuesAsVectors);
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
}