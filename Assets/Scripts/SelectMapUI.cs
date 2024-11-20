using UnityEngine;
using UnityEngine.EventSystems;

public class SelectMapUI : MonoBehaviour, IEndDragHandler
{
    [SerializeField] int maxPage;
    private int currentPage;
    private Vector3 targetPosition;
    private float dragThreshould;

    [SerializeField] Vector3 pageStep;
    [SerializeField] RectTransform levelPagesRect;
    [SerializeField] LeanTweenType tweenType;
    [SerializeField] float tweenTime;

    private void Awake()
    {
        currentPage = 1;
        targetPosition = levelPagesRect.localPosition;
        dragThreshould = Screen.width / 20;
    }
    public void ButtonNext()
    {
        if (currentPage < maxPage)
        {
            currentPage++;
            targetPosition += pageStep;
            MovePage();
        }
    }
    public void ButtonPrevious()
    {
        if (currentPage > 1)
        {
            currentPage--;
            targetPosition -= pageStep;
            MovePage();
        }
    }
    private void MovePage()
    {
        levelPagesRect.LeanMoveLocal(targetPosition, tweenTime).setEase(tweenType);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Mathf.Abs(eventData.position.x - eventData.pressPosition.x) > dragThreshould)
        {
            if (eventData.position.x > eventData.pressPosition.x)
            {
                ButtonPrevious();
            }
            else
            {
                ButtonNext();
            }
        }
        else
        {
            MovePage();
        }
    }
    public void ButtonExit()
    {
        UIManager.Singleton.CloseUI();
    }
}
