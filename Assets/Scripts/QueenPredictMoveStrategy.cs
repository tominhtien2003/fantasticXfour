using UnityEngine;

public class QueenPredictMoveStrategy : IPredictionMovePieceStrategy
{
    // 8 hướng di chuyển (trực tiếp và chéo)
    private Vector3Int[] directions = new Vector3Int[8]
    {
        new Vector3Int(1, 0, 0),
        new Vector3Int(-1, 0, 0),
        new Vector3Int(0, 1, 0),
        new Vector3Int(0, -1, 0),
        new Vector3Int(1, 1, 0),
        new Vector3Int(1, -1, 0),
        new Vector3Int(-1, 1, 0),
        new Vector3Int(-1, -1, 0)
    };

    public void PredictMove()
    {
        BasePiece currentPiece = GameLogic.Instance.GetCurrentPiece();
        PredictMovesForQueen(currentPiece.GetCurrentBlock());
    }

    private void PredictMovesForQueen(Block block)
    {
        Board board = Board.Instance;
        Vector3Int rootPos = block.GetPositionInBoard();

        foreach (var dir in directions)
        {
            PredictInDirection(board, rootPos, dir);
        }
    }

    private void PredictInDirection(Board board, Vector3Int rootPos, Vector3Int dir)
    {
        Vector3Int newPos = rootPos + dir;

        while (true)
        {
            Block nextBlock = board.GetBlockAtPosition(newPos.x, newPos.y, newPos.z);

            if (!HandleBlockSelection(nextBlock, newPos, board))
            {
                break;
            }

            newPos += dir;
        }
    }

    private bool HandleBlockSelection(Block block, Vector3Int currentPos, Board board)
    {
        if (block == null || block.tag == "CanNotMove")
        {
            for (int offset = -5; offset <= 5; offset++)
            {
                Block adjacentBlock = board.GetBlockAtPosition(currentPos.x, currentPos.y, currentPos.z + offset);
                if (adjacentBlock != null && adjacentBlock.tag != "CanNotMove")
                {
                    if (adjacentBlock.GetCurrentPiece() != null && adjacentBlock.GetCurrentPiece().chessSide == GameLogic.Instance.GetCurrentChessSide())
                    {
                        return false;
                    }
                    SelectBlock(adjacentBlock);
                    return adjacentBlock.GetCurrentPiece() == null;
                }
            }
            return false;
        }
        if (block.GetCurrentPiece() != null && block.GetCurrentPiece().chessSide == GameLogic.Instance.GetCurrentChessSide())
        {
            return false;
        }
        SelectBlock(block);
        return block.GetCurrentPiece() == null;
    }

    private void SelectBlock(Block block)
    {
        GameLogic.Instance.blocksSelected.Add(block);
        GameObject selectedObject = ObjectPooler.Instance.GetPoolObject("Selected", new Vector3(0, .53f, 0), Quaternion.identity, block.transform);
        block.SetSelectedObject(selectedObject);
        block.blockState = BlockState.Selected;
    }
}
