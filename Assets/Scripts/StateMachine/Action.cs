using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  abstract class Action : ScriptableObject
{
    public abstract void OnStateEnter(StateMachine stateMachine);
    public abstract void Act (StateMachine stateMachine);
}
