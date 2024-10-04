using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] GameObject selected;

    [SerializeField] GameObject panelUIConfirm;
    private BasePiece currentPiece;
    private Vector3Int positionInBoard;
    private BlockState _blockState;
    public BlockState blockState
    {
        get { return _blockState; }
        set
        {
            if ( _blockState != value )
            {
                _blockState = value;
                OnBlockStateChange();
            }
        }
    }
    private void Awake()
    {
        
    }
    private void Start()
    {
        blockState = BlockState.Normal;
    }
    private void OnBlockStateChange()
    {
        selected?.SetActive(blockState == BlockState.Selected);
    }
    public Vector3Int GetPositionInBoard()
    {
        return positionInBoard;
    }
    public void SetPositionInBoard(Vector3Int newPositionInBoard)
    {
        positionInBoard = newPositionInBoard;
    }
    public GameObject GetPanelUIConfirm()
    {
        return panelUIConfirm;
    }
    public BasePiece GetCurrentPiece()
    {
        return currentPiece;
    }
    public void SetCurrentPiece(BasePiece newPiece)
    {
        currentPiece = newPiece;
    }
}
