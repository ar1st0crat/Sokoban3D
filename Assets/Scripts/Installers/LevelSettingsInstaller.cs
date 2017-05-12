using System;
using Zenject;

public class LevelSettingsInstaller : ScriptableObjectInstaller<LevelSettingsInstaller>
{
    public LevelInstaller.Settings LevelInstaller;
    public GameSettings Game;
    public BoxSettings Box;
    public MoverSettings Mover;

    [Serializable]
    public class GameSettings
    {
        public GameStateLevelStarted.Settings StateLevelStarted;
        public GameStateLevelCompleted.Settings StateLevelCompleted;
        public GameStateLevelStuck.Settings StateLevelStuck;
    }

    [Serializable]
    public class BoxSettings
    {
        public BoxStateMove.Settings StateMove;
        public BoxStateAtRest.Settings StateAtRest;
        public BoxStateFitPlatform.Settings StateFitPlatform;
        public BoxStateStuck.Settings StateStuck;
    }

    [Serializable]
    public class MoverSettings
    {
        public MoverStateAtRest.Settings StateAtRest;
        public MoverStateMove.Settings StateMove;
        public MoverStatePull.Settings StatePull;
        public MoverStatePush.Settings StatePush;
    }

    public override void InstallBindings()
    {
        Container.BindInstance(Game.StateLevelStarted);
        Container.BindInstance(Game.StateLevelCompleted);
        Container.BindInstance(Game.StateLevelStuck);

        Container.BindInstance(Box.StateMove);
        Container.BindInstance(Box.StateAtRest);
        Container.BindInstance(Box.StateFitPlatform);
        Container.BindInstance(Box.StateStuck);

        Container.BindInstance(Mover.StateAtRest);
        Container.BindInstance(Mover.StateMove);
        Container.BindInstance(Mover.StatePull);
        Container.BindInstance(Mover.StatePush);

        Container.BindInstance(LevelInstaller);
    }
}