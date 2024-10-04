using UnityEngine;

public class BishopPredictMoveStrategy : IPredictionMovePieceStrategy
{
    // 4 hướng đi chéo
    private Vector3Int[] directions = new Vector3Int[4]
    {
        new Vector3Int(1, 1, 0),   
        new Vector3Int(1, -1, 0),  
        new Vector3Int(-1, 1, 0),  
        new Vector3Int(-1, -1, 0) 
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
                if (nextBlock == null)
                {
                    break;
                }
                else
                {
                    if (nextBlock.tag == "CanNotMove")
                    {
                        Block aboveBlock = board.GetBlockAtPosition(newPos.x, newPos.y, newPos.z + 1);
                        if (aboveBlock != null && aboveBlock.tag != "CanNotMove")
                        {
                            GameLogic.Instance.blocksSelected.Add(aboveBlock);
                            aboveBlock.blockState = BlockState.Selected;
                            if (aboveBlock.GetCurrentPiece() != null)
                            {
                                break;
                            }
                            else
                            {
                                newPos += dir;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        GameLogic.Instance.blocksSelected.Add(nextBlock);
                        nextBlock.blockState = BlockState.Selected;
                        if (nextBlock.GetCurrentPiece() != null)
                        {
                            break;
                        }
                        else
                        {
                            newPos += dir;
                        }
                    }
                }
            }
        }
    }
}
