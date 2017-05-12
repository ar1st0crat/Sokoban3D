using ModestTree;

public enum GameStates
{
    LevelStarted,
    LevelCompleted,
    LevelStuck,
    Count
}

public class GameStateFactory
{
    readonly GameStateLevelStarted.Factory _levelStartedFactory;
    readonly GameStateLevelCompleted.Factory _levelCompletedFactory;
    readonly GameStateLevelStuck.Factory _levelStuckFactory;

    public GameStateFactory(
        GameStateLevelStarted.Factory levelStartedFactory,
        GameStateLevelCompleted.Factory levelCompletedFactory,
        GameStateLevelStuck.Factory levelStuckFactory)
    {
        _levelStartedFactory = levelStartedFactory;
        _levelCompletedFactory = levelCompletedFactory;
        _levelStuckFactory = levelStuckFactory;
    }

    public GameState CreateState(GameStates state)
    {
        switch (state)
        {
            case GameStates.LevelStarted:
            {
                return _levelStartedFactory.Create();
            }
            case GameStates.LevelCompleted:
            {
                return _levelCompletedFactory.Create();
            }
            case GameStates.LevelStuck:
            {
                return _levelStuckFactory.Create();
            }
        }

        throw Assert.CreateException();
    }
}
