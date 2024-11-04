using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] GameObject selected;

    [SerializeField] GameObject panelUIConfirm;
    private BasePiece currentPiece;
    private Vector3Int positionInBoard;
    private BlockState _blockState;

    private GameObject selectedObject;
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
        //GameObject objectSelected = ObjectPooler.Instance.GetPoolObject("Selected", new Vector3(0, .5f, 0), Quaternion.identity, transform);
        ////objectSelected?.SetActive(blockState == BlockState.Selected);
        if (blockState == BlockState.Normal)
        {
            selectedObject.SetActive(false);
            //selectedObject.transform.SetParent(ObjectPooler.Instance.transform);
        }
        else
        {
            selectedObject.SetActive(true);
        }
        //selectedObject?.SetActive(blockState == BlockState.Selected); 
        //selected?.SetActive(blockState == BlockState.Selected); 
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
    public GameObject GetSelectedObject()
    {
        return selectedObject;
    }
    public void SetSelectedObject(GameObject newSelectedObject)
    {
        selectedObject = newSelectedObject;
    }
}
