using UnityEngine;

public class BasePiece : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] LayerMask groundMask;

    protected Block currentBlock;
    public PieceType pieceType;
    public ChessSide chessSide;

    protected void GetCurrentBlockWhenStartGame()
    {
        if (Physics.Raycast(transform.position,Vector3.down,out RaycastHit hitInfo, .5f, groundMask))
        {
            currentBlock = hitInfo.collider.gameObject.GetComponentInParent<Block>();
            currentBlock.SetCurrentPiece(this);
            //Debug.Log(currentBlock.GetPositionInBoard() + " " +currentBlock);
        }
        else
        {
            Debug.Log("Không có khối dưới quân cờ hiện tại");
        }
    }
    public Block GetCurrentBlock()
    {
        return currentBlock;
    }
    public void SetCurrentBlock(Block newBlock)
    {
        currentBlock = newBlock;
    }
}
