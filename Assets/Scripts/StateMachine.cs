using UnityEngine;

public class StateMachine<T> : MonoBehaviour
{
    private BaseState<T> currentState;

    public void ChangeState(BaseState<T> state)
    {
        if (currentState != null && state.TypeState() == currentState.TypeState())
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
            currentState.Execute();
        }
    }
}
