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
        Vector3Int rootPos = block.GetPositionInBoard(); 

        foreach (var dir in directions)
        {
            Vector3Int newPos = rootPos + dir;

            while (true)
            {
                Block nextBlock = board.GetBlockAtPosition(newPos.x, newPos.y, newPos.z);
                if (nextBlock == null) break;

                if (nextBlock.tag == "CanNotMove")
                {
                    Block aboveBlock = CheckAboveBlock(newPos);
                    if (aboveBlock != null)
                    {
                        AddBlockToSelection(aboveBlock);
                        if (aboveBlock.GetCurrentPiece() != null)
                            break;
                    }
                    else break;
                }
                else
                {
                    AddBlockToSelection(nextBlock);
                    if (nextBlock.GetCurrentPiece() != null)
                        break;
                }
                newPos += dir;
            }
        }
    }

    private Block CheckAboveBlock(Vector3Int pos)
    {
        Board board = Board.Instance;
        Block aboveBlock = board.GetBlockAtPosition(pos.x, pos.y, pos.z + 1);
        if (aboveBlock != null && aboveBlock.tag != "CanNotMove")
        {
            return aboveBlock;
        }
        return null;
    }

    private void AddBlockToSelection(Block block)
    {
        GameLogic.Instance.blocksSelected.Add(block);
        block.blockState = BlockState.Selected;
    }
}
