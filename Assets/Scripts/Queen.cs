public class Queen : BasePiece
{
    private void Start()
    {
        pieceType = PieceType.Queen;
        GetCurrentBlockWhenStartGame();
    }
}
