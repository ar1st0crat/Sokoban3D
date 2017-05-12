using System;
using UnityEngine;

public abstract class BoxState : IDisposable
{
    public abstract void Update();

    public virtual void Start()
    {
        // as in Zenject sample apps
    }

    public virtual void Dispose()
    {
        // as in Zenject sample apps
    }

    public virtual void OnTriggerEnter(Collider collider)
    {
        // as in Zenject sample apps
    }
}
