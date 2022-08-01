using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// What is a State Machine?
/// State Machines are used to control the flow of states.
/// They have info of current state and can switch b/w states.
/// </summary>
public class StateMachine : MonoBehaviour
{
    State currentState = null;

    public void SwitchState(State newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }

    private void Update()
    {
        currentState?.Tick(Time.deltaTime);
    }
}
