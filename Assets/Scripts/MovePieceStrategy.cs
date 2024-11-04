using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MovePieceStrategy : IMovePieceStrategy
{
    private readonly List<Block> blocks = new List<Block>();

    public async void Move()
    {
        Block targetBlock = Board.Instance.GetCurrentBlock();
        BasePiece currentPiece = GameLogic.Instance.GetCurrentPiece();

        Vector3Int startPos = currentPiece.GetCurrentBlock().GetPositionInBoard();
        Vector3Int endPos = targetBlock.GetPositionInBoard();
        Vector3Int dir = GetDirection(startPos, endPos);

        blocks.Clear();
        blocks.Add(currentPiece.GetCurrentBlock());
        await CollectBlocks(startPos, endPos, dir);
        currentPiece.StartCoroutine(MoveAlongPath(currentPiece, dir));
    }

    private Vector3Int GetDirection(Vector3Int startPos, Vector3Int endPos)
    {
        int deltaRow = endPos.x - startPos.x;
        int deltaCol = endPos.y - startPos.y;

        if (deltaRow != 0 && deltaCol == 0) 
        {
            return new Vector3Int((int)Mathf.Sign(deltaRow), 0, 0);
        }
        else if (deltaCol != 0 && deltaRow == 0) 
        {
            return new Vector3Int(0, (int)Mathf.Sign(deltaCol), 0);
        }
        else if (deltaRow != 0 && deltaCol != 0) 
        {
            return new Vector3Int((int)Mathf.Sign(deltaRow), (int)Mathf.Sign(deltaCol), 0);
        }

        Debug.LogWarning("Error: Không có thay đổi trong vị trí. Hãy đảm bảo bạn đang chọn đúng khối đích.");
        return Vector3Int.zero;
    }

    private async Task CollectBlocks(Vector3Int startPos, Vector3Int endPos, Vector3Int dir)
    {
        while (startPos.x != endPos.x || startPos.y != endPos.y)
        {
            startPos += dir;
            Block nextBlock = Board.Instance.GetBlockAtPosition(startPos.x, startPos.y, startPos.z);
            if (!ProcessBlock(nextBlock, startPos))
            {
                break;
            }
        }
        await Task.Yield();
    }

    private bool ProcessBlock(Block block, Vector3Int currentPos)
    {
        if (block == null || block.CompareTag("CanNotMove"))
        {
            return TryAddAdjacentBlock(currentPos);
        }

        blocks.Add(block);
        return block.GetCurrentPiece() == null;
    }

    private bool TryAddAdjacentBlock(Vector3Int pos)
    {
        for (int offset = -5; offset <= 5; offset++)
        {
            Block adjacentBlock = Board.Instance.GetBlockAtPosition(pos.x, pos.y, pos.z + offset);
            if (adjacentBlock != null && !adjacentBlock.CompareTag("CanNotMove"))
            {
                blocks.Add(adjacentBlock);
                return adjacentBlock.GetCurrentPiece() == null;
            }
        }
        return false;
    }

    private IEnumerator MoveAlongPath(BasePiece piece, Vector3Int dir)
    {
        Vector3 offset = new Vector3(0, 0.51f, 0);

        for (int idx = 1; idx < blocks.Count; idx++)
        {
            Vector3 startPos = blocks[idx - 1].transform.position + offset;
            Vector3 endPos = blocks[idx].transform.position + offset;

            if (IsJumping(blocks[idx - 1], blocks[idx]) && !HaveObstacle(blocks[idx], dir))
            {
                Vector3 middlePos = Vector3.Lerp(startPos, endPos, 0.5f) + offset * 2;
                yield return piece.IEJumpCurve(startPos, endPos, middlePos);
            }
            else
            {
                yield return piece.IEMoveFlat(startPos, endPos);
            }
        }
        Block endBlock = blocks[blocks.Count - 1];
        Block startBlock = blocks[0];
        endBlock.GetCurrentPiece()?.TurnOffSelf(2f);
        piece.SetCurrentBlock(endBlock);
        endBlock.SetCurrentPiece(piece);
        startBlock.SetCurrentPiece(null);

    }

    private bool HaveObstacle(Block block, Vector3Int dir)
    {
        Vector3Int pos1 = block.GetPositionInBoard() + new Vector3Int(dir.x, 0, 0);
        Vector3Int pos2 = block.GetPositionInBoard() + new Vector3Int(0, dir.y, 0);

        Block block1 = Board.Instance.GetBlockAtPosition(pos1.x, pos1.y, pos1.z);
        Block block2 = Board.Instance.GetBlockAtPosition(pos2.x, pos2.y, pos2.z);

        return (block1 != null && block1.CompareTag("CanNotMove")) ||
               (block2 != null && block2.CompareTag("CanNotMove"));
    }

    private bool IsJumping(Block startBlock, Block endBlock)
    {
        return startBlock.GetPositionInBoard().z != endBlock.GetPositionInBoard().z;
    }
}
