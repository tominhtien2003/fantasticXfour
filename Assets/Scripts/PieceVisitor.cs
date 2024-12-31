public class PieceVisitor : IVisitor
{
    private Block block;
    public void SetCurrentBlock(Block block)
    {
        this.block = block;
    }
    public void Visit(BaseTrap trap)
    {
        if (block != null && block.GetCurrentPiece() != null)
        {
            block.GetCurrentPiece().SetUpWhenIsTarget();
            block.GetCurrentPiece()?.TurnOffSelf2(.5f);
            block.SetCurrentPiece(null);

            //float timeDelay = 1f;
            //if (GameLogic.Instance.GetTurn() == Turn.Player)
            //{
            //    GameLogic.Instance.Invoke("AutomaticChangeTurn", timeDelay);
            //}
            //else
            //{
            //    GameLogic.Instance.Invoke("AutomaticChangeTurn", 0.5f);
            //}
        }
    }

}
