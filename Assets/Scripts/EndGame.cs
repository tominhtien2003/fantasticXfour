using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public GameObject win, lose, tie, bg, buttons;
    public void Winn()
    {
        win.SetActive(true);
        bg.SetActive(true);
        buttons.SetActive(true );
    }
    public void Losee()
    {
        lose.SetActive(true);
        bg.SetActive(true);
        buttons.SetActive(true);
    }
    public void ButtonNext()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; 
        int nextSceneIndex = currentSceneIndex + 1; 

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
    public void ButtonQuite()
    {
        SceneManager.LoadScene("UI");
    }
    public void PlayAgain()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
