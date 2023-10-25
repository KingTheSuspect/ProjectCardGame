using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public IState CurrentState { get; private set; }

    public StateMachine(IState defaultState = null)
    {
        ChangeState(defaultState);
    }
    public void ChangeState(IState state)
    {
        CurrentState?.Exit();
        state.Enter(this);
        CurrentState = state;
    }
    public void TickState()
    {
        CurrentState?.Tick();
    }
}
