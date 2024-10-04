public class Bishop : BasePiece
{
    private void Start()
    {
        pieceType = PieceType.Bishop;
        GetCurrentBlockWhenStartGame();
    }
}
