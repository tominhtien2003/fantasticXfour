public class PieceIdleState : BaseState
{
    private BasePiece piece;
    public PieceIdleState(BasePiece piece)
    {
        this.piece = piece;
    }
    public void Enter()
    {
        
    }

    public void Excute()
    {
        
    }

    public void Exit()
    {
        
    }

    public string GetTypeState()
    {
        return "Idle";
    }
}
