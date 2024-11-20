using UnityEngine;

public abstract class LevelCompoent : MonoBehaviour
{
    public static int countLevel = 0;   

    public virtual void Add(LevelLeaf component) { }
    public virtual void Remove(LevelLeaf component) { }
}
