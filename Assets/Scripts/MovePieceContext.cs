public class MovePieceContext
{
    private IMovePieceStrategy strategy;
    public void SetStrategy(IMovePieceStrategy newStrategy)
    {
        strategy = newStrategy;
    }
    public void ExcuteStrategy()
    {
        strategy?.Move();
    }
}
