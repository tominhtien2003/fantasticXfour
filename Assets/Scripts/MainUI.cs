using UnityEngine;

public class MainUI : MonoBehaviour
{
    [SerializeField] GameObject SelectMap, Setting;
    public void ButtonPlay()
    {
        UIManager.Singleton.OpenUI(SelectMap, true);
    }
    public void ButtonSetting()
    {
        UIManager.Singleton.OpenUI(Setting, true);
    }
}
