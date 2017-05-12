using ModestTree;

public enum BoxStates
{
    AtRest,
    Move,
    FitPlatform,
    Stuck,
    Count
}

public class BoxStateFactory
{
    readonly BoxStateAtRest.Factory _atRestFactory;
    readonly BoxStateMove.Factory _moveFactory;
    readonly BoxStateFitPlatform.Factory _fitPlatformFactory;
    readonly BoxStateStuck.Factory _stuckFactory;

    public BoxStateFactory(
        BoxStateMove.Factory moveFactory,
        BoxStateAtRest.Factory atRestFactory,
        BoxStateFitPlatform.Factory fitPlatformFactory,
        BoxStateStuck.Factory stuckFactory)
    {
        _moveFactory = moveFactory;
        _atRestFactory = atRestFactory;
        _fitPlatformFactory = fitPlatformFactory;
        _stuckFactory = stuckFactory;
    }

    public BoxState CreateState(BoxStates state)
    {
        switch (state)
        {
            case BoxStates.AtRest:
            {
                return _atRestFactory.Create();
            }
            case BoxStates.Move:
            {
                return _moveFactory.Create();
            }
            case BoxStates.FitPlatform:
            {
                return _fitPlatformFactory.Create();
            }
            case BoxStates.Stuck:
            {
                return _stuckFactory.Create();
            }
        }

        throw Assert.CreateException();
    }
}
