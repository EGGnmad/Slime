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

            case "PunchAttack-Start":
                movement.Punch();
                break;

            case "SwingAttack-Start":
                movement.SwingAttackStart();
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

            case "PunchAttack-Swing":
                movement.PunchSwing();
                break;

            case "SwingAttack-Swing":
                movement.SwingAttackUpdate();
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

            case "PunchAttack-Swing":
                movement.PunchExit();
                break;

            case "SwingAttack-Swing":
                movement.SwingAttackExit();
                break;
        }
    }
}
