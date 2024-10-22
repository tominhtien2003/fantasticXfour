public class PieceMoveState : BaseState
{
    private BasePiece piece;
    public PieceMoveState(BasePiece piece)
    {
        this.piece = piece;
    }

    public void Enter()
    {
        
    }

    public void Excute()
    {
        piece.HandleMovement();
    }

    public void Exit()
    {
        
    }

    public string GetTypeState()
    {
        return "Move";
    }
}
