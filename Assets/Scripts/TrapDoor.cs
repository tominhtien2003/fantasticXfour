using UnityEngine;

public class TrapDoor : BaseTrap
{

    void Awake()
    {
        trapAnim = GetComponent<Animator>();
        traps.Add(this);
    }
    private void Start()
    {
        trapType = TrapType.Door;
    }
}
