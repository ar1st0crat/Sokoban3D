﻿using System;
using UnityEngine;
using Zenject;

/// <summary>
/// TBD
/// </summary>
public class MoverStatePull : MoverState
{
    readonly Settings _settings;
    readonly MoverController _mover;

    public MoverStatePull(
        MoverController mover,
        Settings settings)
    {
        _settings = settings;
        _mover = mover;
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

    public class Factory : Factory<MoverStatePull>
    {
    }
}
