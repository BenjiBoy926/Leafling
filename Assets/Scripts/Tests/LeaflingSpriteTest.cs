using NaughtyAttributes;
using UnityEngine;

public class LeaflingSpriteTest : MonoBehaviour
{
    [SerializeField]
    private LeaflingSprite _sprite;

    private void Reset()
    {
        _sprite = GetComponent<LeaflingSprite>();
    }
    [Button]
    private void DesaturateArms()
    {
        _sprite.DesaturateArmColor();
    }
    [Button]
    private void ResetArms()
    {
        _sprite.ResetArmColor();
    }
}