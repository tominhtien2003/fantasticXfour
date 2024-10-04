using UnityEngine;
using System.Collections;  // Thêm thư viện này để sử dụng Coroutine

public class BlockGFX : MonoBehaviour
{
    private Block logicBlock;
    private Coroutine hidePanelCoroutine;  // Để lưu trữ Coroutine hiện tại

    private void Awake()
    {
        logicBlock = GetComponentInParent<Block>();
    }

    private void OnMouseEnter()
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
            // Bật panel UI
            logicBlock.GetPanelUIConfirm().SetActive(true);

            // Nếu có Coroutine trước đó đang chạy, hủy nó (không cần đếm ngược nếu chuột vẫn ở đây)
            if (hidePanelCoroutine != null)
            {
                StopCoroutine(hidePanelCoroutine);
            }
        }
    }

    private void OnMouseExit()
    {
        // Bắt đầu Coroutine để tắt panel sau 3 giây khi chuột rời khỏi
        hidePanelCoroutine = StartCoroutine(HidePanelAfterDelay(3f));
    }

    // Coroutine để tắt panel sau khoảng thời gian
    private IEnumerator HidePanelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);  // Đợi trong khoảng thời gian (3 giây)
        logicBlock.GetPanelUIConfirm().SetActive(false);  // Tắt PanelUIConfirm
    }
}
