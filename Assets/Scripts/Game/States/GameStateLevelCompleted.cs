using System;
using UnityEngine;
using Zenject;

/// <summary>
/// TBD
/// </summary>
public class GameStateLevelCompleted : GameState
{
    readonly Settings _settings;
    readonly IGameController _game;
    
    public GameStateLevelCompleted(
        IGameController game,
        Settings settings)
    {
        _settings = settings;
        _game = game;
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

    public class Factory : Factory<GameStateLevelCompleted>
    {
    }
}
