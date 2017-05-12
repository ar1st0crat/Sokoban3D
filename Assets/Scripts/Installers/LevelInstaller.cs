using System;
using UnityEngine;
using Zenject;

public class LevelInstaller : MonoInstaller<LevelInstaller>
{
    [Inject]
    Settings _settings = null;

    public override void InstallBindings()
    {
        InstallGame();
        InstallBoxes();
        // TODO: InstallMover();
    }

    void InstallGame()
    {
        Container.Bind<ILevelsConfigurator>().To<LevelsConfigurator>().AsSingle();

        Container.Bind<IGameController>().To<GameController>().AsSingle();

        Container.Bind<GameStateFactory>().AsSingle();

        Container.BindFactory<GameStateLevelStarted, GameStateLevelStarted.Factory>().WhenInjectedInto<GameStateFactory>();
        Container.BindFactory<GameStateLevelCompleted, GameStateLevelCompleted.Factory>().WhenInjectedInto<GameStateFactory>();
        Container.BindFactory<GameStateLevelStuck, GameStateLevelStuck.Factory>().WhenInjectedInto<GameStateFactory>();
    }

    void InstallBoxes()
    {
        Container.Bind<BoxStateFactory>().AsSingle();

        Container.BindFactory<BoxStateMove, BoxStateMove.Factory>().WhenInjectedInto<BoxStateFactory>();
        Container.BindFactory<BoxStateAtRest, BoxStateAtRest.Factory>().WhenInjectedInto<BoxStateFactory>();
        Container.BindFactory<BoxStateFitPlatform, BoxStateFitPlatform.Factory>().WhenInjectedInto<BoxStateFactory>();
        Container.BindFactory<BoxStateStuck, BoxStateStuck.Factory>().WhenInjectedInto<BoxStateFactory>();

        Container.BindFactory<Box, Box.Factory>()
                 .FromComponentInNewPrefab(_settings.BoxPrefab)
                 .WithGameObjectName("Box")
                 .UnderTransformGroup("Boxes");
    }

    void InitExecutionOrder()
    {
        // Left for experiments:
        // Container.BindExecutionOrder<BoxStateFactory>(-10);
        // Container.BindExecutionOrder<GameController>(-20);
    }

    [Serializable]
    public class Settings
    {
        public GameObject MoverPrefab;
        public GameObject BoxPrefab;
        public GameObject WallPrefab;
        public GameObject PlatformPrefab;
    }
}
