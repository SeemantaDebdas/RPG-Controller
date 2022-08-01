using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Why do we need to make state abstract?
/// --> It's because we don't want an instance of state.
/// We can have any state we want: Jump, Walk, Run so on and so forth.
/// What we do is we make Base States for the Player and enemy namely
/// ---------------PlayerBaseState and EnemyBaseState----------------
/// From these base states, other states like: PlayerJumpingState can 
/// inherit.
/// </summary>
public abstract class State
{
    //==============Three states: Enter, Tick to run the main bulk of the logic, and Exit=============
    public abstract void Enter();
    public abstract void Tick(float deltaTime);
    public abstract void Exit();
}
