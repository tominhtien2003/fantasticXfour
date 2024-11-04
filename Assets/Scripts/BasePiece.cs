using System.Collections;
using UnityEngine;

public class BasePiece : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] LayerMask groundMask;

    public Rigidbody rb;

    protected Block currentBlock;
    public PieceType pieceType;
    public ChessSide chessSide;
    private void Awake()
    {
        rb = GetComponentInChildren<Rigidbody>();
    }
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
    public void HandleMovement()
    {
        MovePieceContext context = new MovePieceContext();
        switch (pieceType)
        {
            case PieceType.Knight:
                context.SetStrategy(new KnightMoveStrategy());
                break;
            case PieceType.Rook:
            case PieceType.Bishop:
            case PieceType.Queen:
            case PieceType.King:
                context.SetStrategy(new MovePieceStrategy());
                break;
            default:
                break;
        }
        context.ExcuteStrategy();

        GameLogic.Instance.ClearListBlockSelected(true);
    }
    public void SetUpWhenIsTarget()
    {
        rb.isKinematic = false;
        rb.mass = .1f;
    }
    public void HandleIdle()
    {

    }
    public IEnumerator IEJumpCurve(Vector3 startPos, Vector3 endPos, Vector3 middlePos)
    {
        float elapsedTime = 0f;
        float jumpDuration = .5f;

        while (elapsedTime < jumpDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / jumpDuration;

            Vector3 startCurve = Vector3.Lerp(startPos, middlePos, t);
            Vector3 endCurve = Vector3.Lerp(middlePos, endPos, t);
            Vector3 targetCurve = Vector3.Lerp(startCurve, endCurve, t);


            transform.position = targetCurve;
            yield return null;
        }

        transform.position = endPos;
    }
    public IEnumerator IEMoveFlat(Vector3 startPos,Vector3 endPos)
    {
        float totalDistance = Vector3.Distance(startPos, endPos);
        float elapsedTime = 0f;
        while(elapsedTime < totalDistance / moveSpeed)
        {
            transform.position = Vector3.Lerp(startPos, endPos, elapsedTime * moveSpeed / totalDistance);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = endPos;
        
    }
    public void TurnOffSelf(float timer)
    {
        StartCoroutine(IETurnOffSelf(timer));
    }
    private IEnumerator IETurnOffSelf(float timer)
    {
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(false);
    }
}
