public class PredictionMoveContext
{
    private IPredictionMovePieceStrategy strategy;
    public void SetStrategy(IPredictionMovePieceStrategy newStrategy)
    {
        strategy = newStrategy;
    }
    public void ExcuteStrategy()
    {
        strategy?.PredictMove();
    }
}
