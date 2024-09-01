using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public StateMachine<PlayerController> stateMachine;
    private void Start()
    {
        stateMachine = gameObject.AddComponent<StateMachine<PlayerController>>();
        stateMachine.ChangeState(new PlayerIdleState(this));
    }
}
