using System;
using UnityEngine;
using Zenject;

/// <summary>
/// TBD
/// </summary>
public class BoxStateMove : BoxState
{
    readonly Settings _settings;
    readonly Camera _mainCamera;
    readonly Box _box;

    public BoxStateMove(
        Settings settings,
        Box box,
        [Inject(Id = "Main")]
        Camera mainCamera)
    {
        _box = box;
        _settings = settings;
        _mainCamera = mainCamera;
    }

    public override void Start()
    {
    }

    public override void Update()
    {
    }

    public override void Dispose()
    {
    }

    public override void OnTriggerEnter(Collider other)
    {
    }

    [Serializable]
    public class Settings
    {
        public float moveSpeed;
        public float rotateSpeed;
    }

    public class Factory : Factory<BoxStateMove>
    {
    }
}
