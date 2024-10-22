using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private BaseState currentState;
    public void ChangeState(BaseState state)
    {
        if (currentState != null && state.GetTypeState() == currentState.GetTypeState())
        {
            return;
        }
        if (currentState != null)
        {
            currentState.Exit();
        }
        currentState = state;

        if (currentState != null)
        {
            currentState.Enter();
        }
    }
    private void Update()
    {
        if (currentState != null)
        {
            currentState.Excute();
        }
    }
}