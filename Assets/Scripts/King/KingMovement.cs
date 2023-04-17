using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingMovement : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] private float speed = 3f;
    Vector2 moveVelocity;

    [Header("Move Settings")]
    [SerializeField] private float startRunMinDistance = 1f;
    [SerializeField] private float stopRunMinDistance = 0.2f;

    [Header("Dash")]
    [SerializeField] private float dashingPower = 24f;
    private bool canDash = true;
    private bool isDashing;

    [Header("Dash Settings")]
    [SerializeField] private float startDashMinDistance = 5f;
    [SerializeField] private float stopDashMinDistance = 0.2f;
    [SerializeField] private float backDashDistance = 5f;


    [Header("Skill")]
    [SerializeField] private float comboAttackCooldown = 3f;
    [SerializeField] private float chargeAttackCooldown = 15f;
    [SerializeField] private float dashAttackCooldown = 30f;
    [SerializeField] private float floatingSwordCooldown = 40f;
    [SerializeField] private bool canComboAttack = true;
    [SerializeField] private bool canChargeAttack = true;
    [SerializeField] private bool canDashAttack = true;
    [SerializeField] private bool canFloatingSword = true;
    private bool isAttacking = false;

    [Header("Skill Settings")]
    [SerializeField] private float startComboAttackMaxDistance = 0.7f;
    [SerializeField] private float startChargeAttackMaxDistance = 0.5f;

    [SerializeField] private float comboAttack1Range = 1.3f;
    [SerializeField] private float comboAttack2Range = 1.3f;
    [SerializeField] private float chargeAttackRange = 2f;
    [SerializeField] private float dashAttackRange = 1f;

    [Header("Others")]
    [SerializeField] private TrailRenderer trailRenderer;


    private GameObject player;
    private bool isFacingRight = true;

    private Rigidbody2D rb;
    private Animator animator;
    private KingAttack attack;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        attack = GetComponent<KingAttack>();

        player = GameObject.Find("Player");
    }

    // Idle
    public void ChooseAction()
    {
        Vector3 direction = player.transform.position - transform.position;
        float distance = direction.magnitude;


        if(distance >= startDashMinDistance && !isAttacking && canDashAttack)
        {
            animator.SetBool("run", false);

            animator.SetTrigger("attack1");
            animator.SetBool("dash", true);
        }

        else if(distance <= startComboAttackMaxDistance && !isAttacking && canComboAttack)
        {
            animator.SetBool("run", false);

            animator.SetTrigger("attack2");
        }
        
        else if(distance <= startChargeAttackMaxDistance && !isAttacking && canChargeAttack)
        {
            animator.SetBool("run", false);

            animator.SetTrigger("attack3");
        }

        else if(distance >= startRunMinDistance)
        {
            animator.SetBool("run", true);
        }
    }


    // Move
    public void Run()
    {
        ChooseAction();
        Move();
        Flip();
    }

    public void RunExit()
    {
        rb.velocity = Vector2.zero;
    }

    private void Move()
    {
        Vector3 direction = player.transform.position - transform.position;
        if (direction.magnitude <= stopRunMinDistance)
        {
            animator.SetBool("run", false);
            return;
        }

        direction = direction.normalized;
        rb.velocity = direction * speed;
    }

    private void Flip()
    {
        Vector3 direction = player.transform.position - transform.position;
        if(isFacingRight && direction.x < 0 || !isFacingRight && direction.x > 0)
        {
            isFacingRight = !isFacingRight;

            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }

    // Dash
    public void DashStart()
    {
        if (canDash) return;

        canDash = false;
    }

    public void Dash(Vector2 to)
    {
        Flip();
        Vector2 direction = to - (Vector2)transform.position;

        if (direction.magnitude <= stopDashMinDistance)
        {
            animator.SetBool("dash", false);
            return;
        }

        trailRenderer.emitting = true;

        direction = direction.normalized;
        rb.velocity = direction * dashingPower;
    }

    public void DashExit()
    {
        rb.velocity = Vector2.zero;
        trailRenderer.emitting = false;
        canDash = true;
    }

    public void DashToPlayer()
    {
        Dash(player.transform.position);
    }

// Skill
    // Dash Attack
    public void DashAttackStart()
    {
        canDashAttack = false;
        isAttacking = true;
    }

    public void DashAttackSwing()
    {
        attack.Attack(1, dashAttackRange);
    }

    public void DashAttackExit()
    {
        isAttacking = false;
        StartCoroutine(DashAttackCooldown());
    }

    IEnumerator DashAttackCooldown()
    {
        yield return new WaitForSeconds(dashAttackCooldown);
    
        canDashAttack = true;
    }

    // Combo Attack
    public void ComboAttackStart()
    {
        canComboAttack = false;
        isAttacking = true;
    }

    public void ComboAttack1Swing()
    {
        attack.Attack(1, comboAttack1Range);
    }

    public void ComboAttack2Swing()
    {
        attack.Attack(1, comboAttack2Range);
    }

    public void ComboAttackExit()
    {

        isAttacking = false;
        StartCoroutine(ComboAttackCooldown());
    }

    IEnumerator ComboAttackCooldown()
    {
        yield return new WaitForSeconds(comboAttackCooldown);

        canComboAttack = true;
    }

    // Charge Attack
    public void ChargeAttackStart()
    {
        canChargeAttack = false;
        isAttacking = true;
    }

    public void ChargeAttackSwing()
    {
        attack.Attack(2, chargeAttackCooldown);
    }

    public void ChargeAttackExit()
    {

        isAttacking = false;
        StartCoroutine(ChargeAttackCooldown());
    }

    IEnumerator ChargeAttackCooldown()
    {
        yield return new WaitForSeconds(chargeAttackCooldown);

        canChargeAttack = true;
    }
}
