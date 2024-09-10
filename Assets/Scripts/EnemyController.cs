using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private StateMachine<EnemyController> stateMachine;
    private void Start()
    {
        stateMachine = gameObject.AddComponent<StateMachine<EnemyController>>();
        stateMachine.ChangeState(new EnemyIdleState(this));
    }
}
