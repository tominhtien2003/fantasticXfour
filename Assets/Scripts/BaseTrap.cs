using System.Collections.Generic;
using UnityEngine;

public abstract class BaseTrap : MonoBehaviour
{
    public static List<BaseTrap> traps = new List<BaseTrap>();
    protected Animator trapAnim;
    public void Open()
    {
        trapAnim.SetTrigger("open");
    }
    public void Close()
    {
        trapAnim.SetTrigger("close");
    }
    void OnDisable()
    {
        traps.Clear();
    }
}
