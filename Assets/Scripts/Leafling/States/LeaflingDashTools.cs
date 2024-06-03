using UnityEngine;

namespace Leafling
{
    public class LeaflingDashTools
    {
        public static void SetRotation(Leafling leafling, Vector2 aim)
        {
            Vector2 spriteRight = aim;
            if (spriteRight.x < 0)
            {
                spriteRight *= -1;
            }
            leafling.SetSpriteRight(spriteRight);
        }
    }
}