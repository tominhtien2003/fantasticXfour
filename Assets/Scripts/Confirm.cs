using UnityEngine;

public class Confirm : MonoBehaviour
{
    private void Start()
    {
        transform.forward = Camera.main.transform.forward;
    }
    public void ButtonYes()
    {
        GameLogic.Instance.GetCurrentPiece().HandleMovement();
    }
    public void ButtonNo()
    {
        Block currentBlock = Board.Instance.GetCurrentBlock();
        if (currentBlock!=null)
        {
            currentBlock.GetPanelUIConfirm().SetActive(false);           
        }
    }
}
