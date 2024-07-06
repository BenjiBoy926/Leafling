using System;
using UnityEngine;

public class DashTarget : MonoBehaviour
{
    public event Action Tickled = delegate { };
    public event Action Struck = delegate { };

    public Vector2 Position => transform.position; 
    
    public void Tickle()
    {
        Tickled();
    }
    
    public void Strike()
    {
        Struck();
    }
}
