using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLeaf : MonoBehaviour 
{
    public int numberLevel;
    public bool lockLevel;
    private void Awake()
    {
        lockLevel = true;
    }
    private void Start()
    {
        
    }
    public void Press()
    {
        if (!lockLevel)
        {
            string nameScene = "Map " + numberLevel;
            //SceneTransition.Instance.Transition(nameScene);
            SceneManager.LoadScene(nameScene);
        }
    }
}
