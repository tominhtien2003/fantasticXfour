using UnityEngine;

public class BlockGFX : MonoBehaviour
{
    private Block logicBlock;
    private void Awake()
    {
        logicBlock = GetComponentInParent<Block>();
    }
    private void OnMouseDown()
    {
        if (logicBlock.blockState == BlockState.Normal)
        {
            return;
        }
        Block currentBlock = Board.Instance.GetCurrentBlock();
        if (currentBlock != logicBlock)
        {
            Board.Instance.SetCurrentBlock(logicBlock);
            if (currentBlock != null)
            {
                currentBlock.GetPanelUIConfirm().SetActive(false);
            }
        }
        if (logicBlock.blockState == BlockState.Selected)
        {
            logicBlock.GetPanelUIConfirm().SetActive(!logicBlock.GetPanelUIConfirm().activeSelf);
        }
    }
}
