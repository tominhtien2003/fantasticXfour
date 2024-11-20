using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public Animator sceneAnimation;
    public float transitionDuration = 1f;

    private static SceneTransition instance;
    public static SceneTransition Instance { get { return instance; } }
    public Canvas canvas;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Transition(string targetScene)
    {
        StartCoroutine(PerformTransition(targetScene));
    }

    private IEnumerator PerformTransition(string targetScene)
    {
        canvas.sortingOrder = 1;
        if (sceneAnimation != null)
        {
            sceneAnimation.SetTrigger("LightDark");
        }

        yield return new WaitForSeconds(.9f);

        SceneManager.LoadScene(targetScene);

        if (sceneAnimation != null)
        {
            sceneAnimation.SetTrigger("DarkLight");
        }
        canvas.sortingOrder = 0;
    }
}
