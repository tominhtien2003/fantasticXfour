using UnityEngine;

public class Confirm : MonoBehaviour
{
    [SerializeField] private Block blockParent;

    private void Start()
    {
        blockParent = GetComponentInParent<Block>();
        
    }

    private void Update()
    {
        transform.forward = Camera.main.transform.forward;
        Block currentBlock = Board.Instance.GetCurrentBlock();

        if (blockParent == currentBlock)
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                blockParent.GetCurrentPiece()?.SetUpWhenIsTarget();
                ButtonYes();
            }
            else if (Input.GetKeyDown(KeyCode.X))
            {
                ButtonNo();
            }
        }
    }

    public void ButtonYes()
    {
        GameLogic.Instance.GetCurrentPiece().HandleMovement();
    }

    public void ButtonNo()
    {
        Block currentBlock = Board.Instance.GetCurrentBlock();

        if (currentBlock != null)
        {
            currentBlock.GetPanelUIConfirm().SetActive(false);
        }
    }
}
