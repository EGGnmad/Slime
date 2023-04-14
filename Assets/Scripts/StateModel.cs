using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateModel : MonoBehaviour
{
    public abstract void StateEnter(string stateName);
    public virtual void StateUpdate(string stateName) { }
    public abstract void StateExit(string stateName);
}
