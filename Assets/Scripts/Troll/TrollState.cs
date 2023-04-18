using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrollState : StateModel
{
    private TrollMovement movement;

    private void Start()
    {
        movement = GetComponent<TrollMovement>();
    }

    public override void StateEnter(string stateName)
    {
        switch (stateName)
        {
            case "GroundAttack-Start":
                movement.GroundAttack();
                break;
            case "GroundAttack-Swing":
                movement.GroundAttackSwingStart();
                break;
        }
    }

    public override void StateUpdate(string stateName)
    {
        switch (stateName)
        {
            case "Idle":
                movement.ChooseAction();
                break;
            case "Walk":
                movement.Walk();
                break;

            case "GroundAttack-Swing":
                movement.GroundAttackSwing();
                break;
        }
    }

    public override void StateExit(string stateName)
    {
        switch (stateName)
        {
            case "Walk":
                movement.WalkExit();
                break;
            case "GroundAttack-Swing":
                movement.GroundAttackExit();
                break;
        }
    }
}
