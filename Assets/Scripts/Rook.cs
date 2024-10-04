public class Rook : BasePiece
{
    private void Start()
    {
        pieceType = PieceType.Rook;
        GetCurrentBlockWhenStartGame();
    }
}
