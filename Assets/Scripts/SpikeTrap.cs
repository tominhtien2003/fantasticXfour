using UnityEngine;
public class SpikeTrap : BaseTrap
{
    void Awake()
    {
        trapAnim = GetComponent<Animator>();
        traps.Add(this);
    }
}
