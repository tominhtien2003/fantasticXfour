public abstract class BaseState<T> 
{
    protected T controller;

    public BaseState(T controller)
    {
        this.controller = controller;
    }
    public abstract void Enter();
    public abstract void Execute();
    public abstract void Exit();
    public abstract string TypeState();
}
