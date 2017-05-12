using System;
using UnityEngine;
using Zenject;

/// <summary>
/// TBD
/// </summary>
public class BoxStateAtRest : BoxState
{
    readonly Settings _settings;
    readonly Box _box;

    public BoxStateAtRest(
        Box box,
        Settings settings)
    {
        _settings = settings;
        _box = box;
    }

    public override void Start()
    {
    }

    public override void Update()
    {
    }

    [Serializable]
    public class Settings
    {
        public Vector3 StartOffset;
    }

    public class Factory : Factory<BoxStateAtRest>
    {
    }
}
