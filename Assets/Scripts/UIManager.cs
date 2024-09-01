using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject uiLobby;
    public static UIManager Singleton { get; private set; }
    public Stack<GameObject> storedUI = new Stack<GameObject>();
    private void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        OpenUI(uiLobby, false);
    }
    public void OpenUI(GameObject uiGameobject, bool islockUIPrevious)
    {
        if (islockUIPrevious)
        {
            if (storedUI.Count > 0)
            {
                storedUI.Peek().SetActive(false);
            }
        }
        if (uiGameobject != null)
        {
            storedUI.Push(uiGameobject);
            uiGameobject.SetActive(true);
        }
        else
        {
            GameObject currentUIGameobject = storedUI.Peek();
            if (currentUIGameobject != null)
            {
                currentUIGameobject.SetActive(false);
            }
            else
            {
                Debug.Log("currentUIGameobject is null");
            }
        }
    }
    public void CloseUI()
    {
        if (storedUI.Count == 0)
        {
            Debug.LogWarning("No UI to close, stack is empty.");
            return;
        }

        GameObject currentUI = storedUI.Pop();
        if (currentUI != null)
        {
            currentUI.SetActive(false);
        }
        else
        {
            Debug.LogWarning("currentUI is null.");
        }

        if (storedUI.Count > 0)
        {
            GameObject previousUI = storedUI.Peek();
            if (previousUI != null)
            {
                previousUI.SetActive(true);
            }
        }
    }
}
