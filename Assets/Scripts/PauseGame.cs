using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    [SerializeField] GameObject PausePanel;
    public void ButtonPause()
    {
        Time.timeScale = 0;
        GameLogic.Instance.pauseGame = true;
        PausePanel.SetActive(true);
    }
    public void ButtonResume()
    {
        GameLogic.Instance.pauseGame = false;
        Time.timeScale = 1;
        PausePanel.SetActive(false);
    }
    public void ButtonQuite()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("UI");
    }
}
