using UnityEngine;

public class KingPredictMoveStrategy : IPredictionMovePieceStrategy
{
    //row,column,height
    private Vector3Int[] directions = new Vector3Int[8]
    {
        new Vector3Int(1, 0, 0),   //forward
        new Vector3Int(-1, 0, 0),  //backward
        new Vector3Int(0, 1, 0),   //right
        new Vector3Int(0, -1, 0),  //left
        new Vector3Int(1, 1, 0),   //forwardRight
        new Vector3Int(1, -1, 0),  //forwardLeft
        new Vector3Int(-1, 1, 0),  //BackwardRight
        new Vector3Int(-1, -1, 0)  //BackwardLeft
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
            Block adjacentBlock = board.GetBlockAtPosition(newPos.x, newPos.y, newPos.z);

            if (adjacentBlock == null) continue;

            int height = GetHeightAtPosition(newPos, board); // Kiểm tra độ cao tại vị trí mới

            if (height == 1)
            {
                GameLogic.Instance.blocksSelected.Add(adjacentBlock);
                adjacentBlock.blockState = BlockState.Selected;
            }
            else if (height == 2)
            {
                Block topBlock = board.GetBlockAtPosition(newPos.x, newPos.y, newPos.z + 1);
                if (topBlock != null)
                {
                    GameLogic.Instance.blocksSelected.Add(topBlock);
                    topBlock.blockState = BlockState.Selected;
                }
            }
        }
    }

    private int GetHeightAtPosition(Vector3Int position, Board board)
    {
        int height = 1;
        Vector3Int checkPos = position;

        while (board.GetBlockAtPosition(checkPos.x, checkPos.y, checkPos.z + 1) != null)
        {
            height++;
            checkPos.z += 1;
            if (height == 3) break;
        }

        return height;
    }
}
