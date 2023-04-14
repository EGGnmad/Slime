using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class StateCotroller : StateMachineBehaviour
{
    public string stateName;
    StateModel stateObject;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateObject == null)
            stateObject = animator.GetComponent<StateModel>();

        Debug.Assert(stateObject, "Can not found state model!");

        stateObject.StateEnter(stateName);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateObject == null)
            stateObject = animator.GetComponent<StateModel>();

        Debug.Assert(stateObject, "Can not found state model!");

        stateObject.StateUpdate(stateName);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateObject == null)
            stateObject = animator.GetComponent<StateModel>();

        Debug.Assert(stateObject, "Can not found state model!");

        stateObject.StateExit(stateName);
    }
}
