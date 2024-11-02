using UnityEngine;

public class BishopPredictMoveStrategy : IPredictionMovePieceStrategy
{
    // 4 hướng đi chéo
    private Vector3Int[] directions = new Vector3Int[4]
    {
        new Vector3Int(1, 1, 0),    // Diagonal right up
        new Vector3Int(1, -1, 0),   // Diagonal right down
        new Vector3Int(-1, 1, 0),   // Diagonal left up
        new Vector3Int(-1, -1, 0)   // Diagonal left down
    };

    public void PredictMove()
    {
        BasePiece currentPiece = GameLogic.Instance.GetCurrentPiece();
        PredictMovesForBishop(currentPiece.GetCurrentBlock());
    }

    private void PredictMovesForBishop(Block block)
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
        GameObject selectedObject = ObjectPooler.Instance.GetPoolObject("Selected", new Vector3(0, .51f, 0), Quaternion.identity, block.transform);
        block.SetSelectedObject(selectedObject);
        block.blockState = BlockState.Selected;
    }
}
