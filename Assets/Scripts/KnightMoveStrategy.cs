using UnityEngine;

public class KnightMoveStrategy : IMovePieceStrategy
{
    public void Move()
    {
        BasePiece currentPiece = GameLogic.Instance.GetCurrentPiece();
        Block targetBlock = Board.Instance.GetCurrentBlock();
        Block startBlock = currentPiece.GetCurrentBlock();
        
        Vector3 offset = new Vector3(0f, .51f, 0f);
        Vector3 startPos = startBlock.transform.position + offset;
        Vector3 endPos = targetBlock.transform.position + offset;
        Vector3 middlePos = Vector3.Lerp(startPos, endPos, .5f) + offset * 5;

        if (currentPiece.pieceType == PieceType.Knight)
        {
            currentPiece.StartCoroutine(currentPiece.IEJumpCurve(startPos, endPos, middlePos));
        }
        targetBlock.GetCurrentPiece()?.TurnOffSelf(2f);
        currentPiece.SetCurrentBlock(targetBlock);
        targetBlock.SetCurrentPiece(currentPiece);
        startBlock.SetCurrentPiece(null);

        
    }
}
