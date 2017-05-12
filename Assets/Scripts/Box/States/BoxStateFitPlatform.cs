using System;
using Zenject;

/// <summary>
/// TBD
/// </summary>
public class BoxStateFitPlatform : BoxState
{
    readonly Settings _settings;
    readonly Box _box;

    public BoxStateFitPlatform(
        Settings settings,
        Box box)
    {
        _settings = settings;
        _box = box;
    }

    public override void Start()
    {
    }

    public override void Dispose()
    {
    }

    public override void Update()
    {
    }

    [Serializable]
    public class Settings
    {
        public float foo;
    }

    public class Factory : Factory<BoxStateFitPlatform>
    {
    }
}
