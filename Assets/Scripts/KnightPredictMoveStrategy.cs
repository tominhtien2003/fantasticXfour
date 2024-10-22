using UnityEngine;

public class KnightPredictMoveStrategy : IPredictionMovePieceStrategy
{
    // 8 hướng di chuyển theo hình chữ L
    private Vector3Int[] directions = new Vector3Int[8]
    {
        new Vector3Int(2, 1, 0),    // 2 ô theo chiều X và 1 ô theo chiều Y
        new Vector3Int(2, -1, 0),
        new Vector3Int(-2, 1, 0),
        new Vector3Int(-2, -1, 0),
        new Vector3Int(1, 2, 0),    // 1 ô theo chiều X và 2 ô theo chiều Y
        new Vector3Int(1, -2, 0),
        new Vector3Int(-1, 2, 0),
        new Vector3Int(-1, -2, 0)
    };

    public void PredictMove()
    {
        BasePiece currentPiece = GameLogic.Instance.GetCurrentPiece();
        PredictMovesForKnight(currentPiece.GetCurrentBlock());
    }

    private void PredictMovesForKnight(Block block)
    {
        Board board = Board.Instance;
        Vector3Int rootPos = block.GetPositionInBoard();

        foreach (var dir in directions)
        {
            Vector3Int newPos = rootPos + dir;
            for (int i = -2; i <= 2; i++)
            {
                Block aboveBlock = board.GetBlockAtPosition(newPos.x, newPos.y, newPos.z + i);
                if (AddBlockIfValid(aboveBlock)) break;
            }
        }
    }

    private bool AddBlockIfValid(Block block)
    {
        if (block == null || block.tag == "CanNotMove") return false;

        GameLogic.Instance.blocksSelected.Add(block);

        GameObject selectedObject = ObjectPooler.Instance.GetPoolObject("Selected", new Vector3(0, .5f, 0), Quaternion.identity, block.transform);
        block.SetSelectedObject(selectedObject);
        block.blockState = BlockState.Selected;

        return true;
    }
}
