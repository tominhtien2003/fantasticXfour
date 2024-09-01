using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField] GameObject uiGameobjectOpen;
    public void ButtonOpenUIGameobject()
    {
        UIManager.Singleton.OpenUI(uiGameobjectOpen, true);
    }
    public void ButtonExit()
    {
        UIManager.Singleton.CloseUI();
    }
}
