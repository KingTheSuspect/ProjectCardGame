using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    StateMachine StateMachine { get; set; }
    void Enter(StateMachine stateMachine);
    void Exit();
    void Tick();
}
