public abstract class State<T>
{
    protected T StateTarget;
    public StateStatus Status;

    public State(T stateTarget)
    {
        this.StateTarget = stateTarget;
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}