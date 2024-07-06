using System;
using UnityEngine;

public class DashTarget : MonoBehaviour
{
    public event Action<DashTargeter> Tickled = delegate { };
    public event Action<DashTargeter> Struck = delegate { };

    public Vector2 Position => transform.position; 
    
    public void Tickle(DashTargeter targeter)
    {
        Tickled(targeter);
    }
    
    public void Strike(DashTargeter targeter)
    {
        Struck(targeter);
    }
}
