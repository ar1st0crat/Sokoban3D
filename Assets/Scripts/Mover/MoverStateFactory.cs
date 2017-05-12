using ModestTree;

public enum MoverStates
{
    AtRest,
    Move,
    Pull,
    Push,
    Count
}

public class MoverStateFactory
{
    readonly MoverStateAtRest.Factory _atRestFactory;
    readonly MoverStateMove.Factory _moveFactory;
    readonly MoverStatePull.Factory _pullFactory;
    readonly MoverStatePush.Factory _pushFactory;

    public MoverStateFactory(
        MoverStateAtRest.Factory atRestFactory,
        MoverStateMove.Factory moveFactory,
        MoverStatePull.Factory pullFactory,
        MoverStatePush.Factory pushFactory)
    {
        _atRestFactory = atRestFactory;
        _moveFactory = moveFactory;
        _pullFactory = pullFactory;
        _pushFactory = pushFactory;
    }

    public MoverState CreateState(MoverStates state)
    {
        switch (state)
        {
            case MoverStates.AtRest:
            {
                return _atRestFactory.Create();
            }
            case MoverStates.Move:
            {
                return _moveFactory.Create();
            }
            case MoverStates.Pull:
            {
                return _pullFactory.Create();
            }
            case MoverStates.Push:
            {
                return _pushFactory.Create();
            }
        }

        throw Assert.CreateException();
    }
}
