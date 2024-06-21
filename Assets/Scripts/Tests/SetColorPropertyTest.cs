using NaughtyAttributes;
using UnityEngine;

public class SetColorPropertyTest : MonoBehaviour
{
    [SerializeField]
    private Renderer _renderer;
    [SerializeField]
    private Color _colorSetInCode;
    [SerializeField]
    private Color _colorShownByShader;

    private void Reset()
    {
        _renderer = GetComponent<Renderer>();
    }
    [Button]
    private void OnValidate()
    {
        _renderer.sharedMaterial.SetColor("_Color", _colorSetInCode);
    }
}