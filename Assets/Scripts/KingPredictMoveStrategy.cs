using UnityEngine;

public class KingPredictMoveStrategy : IPredictionMovePieceStrategy
{
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
        var currentPiece = GameLogic.Instance.GetCurrentPiece();
        var currentBlock = currentPiece.GetCurrentBlock();
        PredictMovesForKing(currentBlock);
    }

    private void PredictMovesForKing(Block block)
    {
        var board = Board.Instance;
        var rootPos = block.GetPositionInBoard();

        foreach (var dir in directions)
        {
            Vector3Int newPos = rootPos + dir;
            Block adjacentBlock = board.GetBlockAtPosition(newPos.x, newPos.y, newPos.z);

            if (adjacentBlock == null && newPos.z == 0) continue;

            HandleBlockSelection(newPos, adjacentBlock, board);
        }
    }
    private void HandleBlockSelection(Vector3Int newPos, Block block, Board board)
    {
        for (int offset = -5; offset <= 5; offset++)
        {
            Vector3Int checkPos = new Vector3Int(newPos.x, newPos.y, newPos.z + offset);
            Block targetBlock = board.GetBlockAtPosition(checkPos.x, checkPos.y, checkPos.z);

            if (targetBlock != null && targetBlock.tag != "CanNotMove" )
            {
                if (targetBlock.GetCurrentPiece() != null && targetBlock.GetCurrentPiece().chessSide == GameLogic.Instance.GetCurrentChessSide())
                {
                    break;
                }

                SelectBlock(targetBlock);
            }
        }
    }
    private void SelectBlock(Block block)
    {
        GameLogic.Instance.blocksSelected.Add(block);

        GameObject selectedObject = ObjectPooler.Instance.GetPoolObject("Selected", new Vector3(0, .51f, 0), Quaternion.identity, block.transform);
        block.SetSelectedObject(selectedObject);
        block.blockState = BlockState.Selected;
    }

}
