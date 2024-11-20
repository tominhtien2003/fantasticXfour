using System.Collections.Generic;
using UnityEngine;

public static class DirectionOfPieces
{
    private static readonly Vector2Int RowLeft = new Vector2Int(-1, 0);
    private static readonly Vector2Int RowRight = new Vector2Int(1, 0);
    private static readonly Vector2Int ColumnUp = new Vector2Int(0, 1);
    private static readonly Vector2Int ColumnDown = new Vector2Int(0, -1);
    private static readonly Vector2Int DiagonalRightUp = RowRight + ColumnUp;
    private static readonly Vector2Int DiagonalLeftUp = RowLeft + ColumnUp;
    private static readonly Vector2Int DiagonalRightDown = RowRight + ColumnDown;
    private static readonly Vector2Int DiagonalLeftDown = RowLeft + ColumnDown;

    private static readonly Dictionary<PieceType, List<Vector2Int>> Directions = new Dictionary<PieceType, List<Vector2Int>>()
    {
        {
            PieceType.Rook, new List<Vector2Int>
            {
                RowLeft, RowRight, ColumnUp, ColumnDown
            }
        },
        {
            PieceType.King, new List<Vector2Int>
            {
                RowLeft, RowRight, ColumnUp, ColumnDown,
                DiagonalLeftUp, DiagonalRightUp, DiagonalLeftDown, DiagonalRightDown
            }
        },
        {
            PieceType.Queen, new List<Vector2Int>
            {
                RowLeft, RowRight, ColumnUp, ColumnDown,
                DiagonalLeftUp, DiagonalRightUp, DiagonalLeftDown, DiagonalRightDown
            }
        },
        {
            PieceType.Bishop, new List<Vector2Int>
            {
                DiagonalLeftUp, DiagonalRightUp, DiagonalLeftDown, DiagonalRightDown
            }
        },
        {
            PieceType.Knight, new List<Vector2Int>
            {
                new Vector2Int(2, -1), new Vector2Int(2, 1),
                new Vector2Int(-2, 1), new Vector2Int(-2, -1),
                new Vector2Int(1, 2), new Vector2Int(-1, 2),
                new Vector2Int(-1, -2), new Vector2Int(1, -2)
            }
        }
    };

    public static List<Vector2Int> GetDirectionOfPiece(BasePiece piece)
    {
        if (piece == null)
        {
            Debug.LogWarning("Piece is null. Returning empty direction list.");
            return new List<Vector2Int>();
        }

        if (Directions.TryGetValue(piece.pieceType, out List<Vector2Int> directions))
        {
            return directions;
        }

        Debug.LogWarning($"No directions defined for piece type {piece.pieceType}. Returning empty direction list.");
        return new List<Vector2Int>();
    }
}
