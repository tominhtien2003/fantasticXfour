using UnityEngine;
using System.Collections;

public class BlockGFX : MonoBehaviour
{
    private Block logicBlock;
    private Coroutine hidePanelCoroutine;
    private bool isMouseOver = false;
    public LayerMask groundMask;

    private void Awake()
    {
        logicBlock = GetComponentInParent<Block>();
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit , Mathf.Infinity,groundMask))
        {
            if (hit.transform == transform)
            {
                if (!isMouseOver)
                {
                    HandleMouseEnter();
                }
                isMouseOver = true;
            }
            else
            {
                if (isMouseOver)
                {
                    HandleMouseExit();
                }
                isMouseOver = false;
            }
        }
        else if (isMouseOver)
        {
            HandleMouseExit();
            isMouseOver = false;
        }
    }

    private void HandleMouseEnter()
    {
        if (logicBlock.blockState == BlockState.Normal) return;

        Block currentBlock = Board.Instance.GetCurrentBlock();

        if (currentBlock != logicBlock)
        {
            Board.Instance.SetCurrentBlock(logicBlock);

            if (currentBlock != null && currentBlock.GetPanelUIConfirm().activeSelf)
            {
                currentBlock.GetPanelUIConfirm().SetActive(false);
            }
        }

        if (logicBlock.blockState == BlockState.Selected)
        {
            logicBlock.GetPanelUIConfirm().SetActive(true);

            if (hidePanelCoroutine != null)
            {
                StopCoroutine(hidePanelCoroutine);
                hidePanelCoroutine = null;
            }
        }
    }

    private void HandleMouseExit()
    {
        if (hidePanelCoroutine != null)
        {
            StopCoroutine(hidePanelCoroutine);
        }
        hidePanelCoroutine = StartCoroutine(HidePanelAfterDelay(3f));
    }

    private IEnumerator HidePanelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        logicBlock.GetPanelUIConfirm().SetActive(false);
    }
}
