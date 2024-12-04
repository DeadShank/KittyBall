using System.Collections.Generic;
using UnityEngine;

public abstract class FSMBehaviour<T> : MonoBehaviour
{
    [SerializeField] private string currentStateName;

    private State<T> currentState;
    protected Dictionary<State<T>, State<T>> Transitions = new Dictionary<State<T>, State<T>>();

    protected virtual void Start()
    {
        InitStates();
    }

    private void Update()
    {
        currentState?.Update();
        if (currentState?.Status.Equals(StateStatus.Exit) ?? false)
        {
           var nextState = Transitions[currentState];
           ChangeState(nextState);
        }
    }

    public void ChangeState(State<T> newState)
    {
        currentState?.Exit();

        currentState = newState;
        currentStateName = newState.GetType().Name;
        currentState?.Enter();
    }

    protected abstract void InitStates();
}