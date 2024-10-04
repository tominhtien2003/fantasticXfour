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
        GetBlocksPredict(currentPiece.GetCurrentBlock());
    }

    private void GetBlocksPredict(Block block)
    {
        Board board = Board.Instance;
        Vector3Int rootPos = block.GetPositionInBoard();  

        foreach (var dir in directions)
        {
            Vector3Int newPos = rootPos + dir;
            Block nextBlock = board.GetBlockAtPosition(newPos.x, newPos.y, newPos.z);

            if (nextBlock != null)
            {
                if (nextBlock.tag != "CanNotMove")
                {
                    GameLogic.Instance.blocksSelected.Add(nextBlock);
                    nextBlock.blockState = BlockState.Selected;

                }
                else
                {
                    nextBlock = board.GetBlockAtPosition(newPos.x,newPos.y, newPos.z + 1);
                    if (nextBlock.tag != "CanNotMove")
                    {
                        GameLogic.Instance.blocksSelected.Add(nextBlock);
                        nextBlock.blockState = BlockState.Selected;
                    }
                    else
                    {

                    }
                }
            }
        }
    }
}
