using UnityEngine;

public class Confirm : MonoBehaviour
{
    [SerializeField] private Block blockParent;

    private void Start()
    {
        blockParent = GetComponentInParent<Block>();
        transform.forward = Camera.main.transform.forward;
    }

    private void Update()
    {
        Block currentBlock = Board.Instance.GetCurrentBlock();

        if (blockParent == currentBlock)
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
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
