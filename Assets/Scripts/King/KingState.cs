using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingState : StateModel
{
    private KingMovement movement;

    public void Start()
    {
        movement = GetComponent<KingMovement>();
    }

    public override void StateEnter(string stateName)
    {
        switch (stateName)
        {
            case "Dash":
                movement.DashStart();
                break;

            // Dash Attack
            case "DashAttack-Start":
                movement.DashAttackStart();
                break;

            // Combo Attack
            case "ComboAttack-Start":
                movement.ComboAttackStart();
                break;

            // Charge Attack
            case "ChargeAttack-Start":
                movement.ChargeAttackStart();
                break;

            case "FloatingSwordAttack-Start":
                movement.FloatingSwordAttack1();
                break;

            case "Death":
                movement.Death();
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
            case "Run":
                movement.Run();
                break;
            case "Dash":
                movement.DashToPlayer();
                break;

            // Dash Attack
            case "DashAttack-Swing":
                movement.DashAttackSwing();
                break;

            // Combo Attack
            case "ComboAttack1-Swing":
                movement.ComboAttack1Swing();
                break;
            case "ComboAttack2-Swing":
                movement.ComboAttack1Swing();
                break;

            // Charge Attack
            case "ChargeAttack-Swing":
                movement.ChargeAttackSwing();
                break;
        }
    }

    public override void StateExit(string stateName)
    {
        switch (stateName)
        {
            case "Run":
                movement.RunExit();
                break;
            case "Dash":
                movement.DashExit();
                break;


            case "DashAttack-Swing":
                movement.DashAttackExit();
                break;
            case "ComboAttack2-Swing":
                movement.ComboAttackExit();
                break;
            case "ChargeAttack-Swing":
                movement.ChargeAttackExit();
                break;
        }
    }
}
