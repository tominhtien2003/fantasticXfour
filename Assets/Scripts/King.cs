public class King : BasePiece
{
    private void Start()
    {
        pieceType = PieceType.King;
        GetCurrentBlockWhenStartGame();
    }
}
