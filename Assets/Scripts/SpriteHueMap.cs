using System;
using System.Collections.Generic;
using System.Linq;
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

        private readonly int _mapSizeID = Shader.PropertyToID("_MapSize");
        private readonly int _mapKeysID = Shader.PropertyToID("_MapKeys");
        private readonly int _mapValuesID = Shader.PropertyToID("_MapValues");
        private MaterialPropertyBlock _propertyBlock;
        private List<Vector4> _keysAsVectors = new();
        private List<Vector4> _valuesAsVectors = new();

        private void OnValidate()
        {
            if (!Application.isPlaying || _propertyBlock == null) return;
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

        private void RefreshShader()
        {
            LoadColorsAsVectors();
            _renderer.GetPropertyBlock(_propertyBlock);
            _propertyBlock.SetInt(_mapSizeID, MapSize);
            _propertyBlock.SetVectorArray(_mapKeysID, _keysAsVectors);
            _propertyBlock.SetVectorArray(_mapValuesID, _valuesAsVectors);
            _renderer.SetPropertyBlock(_propertyBlock);
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
    }
}