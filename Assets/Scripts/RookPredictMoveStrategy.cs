using UnityEngine;

public class RookPredictMoveStrategy : IPredictionMovePieceStrategy
{
    // Các hướng đi (lên, xuống, trái, phải)
    private Vector3Int[] directions = new Vector3Int[4]
    {
        new Vector3Int(1, 0, 0),   //forward
        new Vector3Int(-1, 0, 0),  //backward
        new Vector3Int(0, 1, 0),   //right
        new Vector3Int(0, -1, 0),  //left
    };

    public void PredictMove()
    {
        BasePiece currentPiece = GameLogic.Instance.GetCurrentPiece();
        GetBlocksPredict(currentPiece.GetCurrentBlock());
    }

    private void GetBlocksPredict(Block block)
    {
        Board board = Board.Instance;
        Vector3Int rootPos = block.GetPositionInBoard();  // Vị trí hiện tại của quân cờ

        // Duyệt qua từng hướng
        foreach (var dir in directions)
        {
            Vector3Int newPos = rootPos + dir;

            while (board.GetBlockAtPosition(newPos.x, newPos.y, newPos.z) != null)
            {
                Block nextBlock = board.GetBlockAtPosition(newPos.x, newPos.y, newPos.z);

                if (nextBlock.tag == "CanNotMove")
                {
                    // Nếu gặp vật cản, kiểm tra khối phía trên
                    Block aboveBlock = board.GetBlockAtPosition(newPos.x, newPos.y, newPos.z + 1);
                    if (aboveBlock != null && aboveBlock.tag != "CanNotMove")
                    {
                        GameLogic.Instance.blocksSelected.Add(aboveBlock);
                        aboveBlock.blockState = BlockState.Selected;
                    }
                }
                else
                {
                    // Nếu không có vật cản, đánh dấu ô hiện tại là có thể di chuyển tới
                    GameLogic.Instance.blocksSelected.Add(nextBlock);
                    nextBlock.blockState = BlockState.Selected;
                }
                newPos += dir;
            }
        }
    }
}
