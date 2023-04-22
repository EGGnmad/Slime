using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrollMovement : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] private float speed = 3f;
    Vector2 moveVelocity;

    [Header("Move Settings")]
    [SerializeField] private float startRunMinDistance = 1f;
    [SerializeField] private float stopRunMinDistance = 0.2f;

    //[Header("Dash")]
    //[SerializeField] private float dashingPower = 24f;
    //private bool canDash = true;
    //private bool isDashing;

    //[Header("Dash Settings")]
    //[SerializeField] private int dashCnt = 2;
    //[SerializeField] private float dashAttackRange = 1f;
    //[SerializeField] private float dashDistance = 6f;
    //[SerializeField] private float startDashMinDistance = 5f;
    //[SerializeField] private float stopDashMinDistance = 0.2f;

    [Header("Skill")]
    [SerializeField] private float groundAttackCooldown = 1f;
    [SerializeField] private float groundUpCooldown = 10f;
    [SerializeField] private float punchAttackCooldown = 3f;
    [SerializeField] private float swingAttackCooldown = 4f;
    [SerializeField] private bool canGroundAttack = true;
    [SerializeField] private bool canGroundUp = true;
    [SerializeField] private bool canPunchAttack = true;
    [SerializeField] private bool canSwingAttack = true;
    private bool isBerserker = false;

    private bool isAttacking = false;


    [Header("Skill Settings")]
    [SerializeField] private float startGroundAttackMaxDistance = 2f;
    [SerializeField] private float startPunchAttackMaxDistance = 1.5f;
    [SerializeField] private float startSwingAttackMaxDistance = 2f;
    [SerializeField] private float groundAttackRange = 3f;
    [SerializeField] private float punchAttackRange = 3f;
    [SerializeField] private float swingAttackRange = 3f;


    [Header("Others")]


    [SerializeField] private GameObject player;
    private bool isFacingRight = true;

    private Rigidbody2D rb;
    private TrollModel model;
    private Animator animator;
    private TrollAttack attack;
    private GroundUpGenerator groundGenerator;
    private SpriteRenderer sprite;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        model = GetComponent<TrollModel>();
        attack = GetComponent<TrollAttack>();
        groundGenerator = GetComponent<GroundUpGenerator>();
        sprite = GetComponent<SpriteRenderer>();
    }


    // Idle
    public void ChooseAction()
    {
        Vector3 direction = player.transform.position - transform.position;
        float distance = direction.magnitude;

        if(model.hp <= (int)(model.maxHp/3) && !isBerserker)
        {
            sprite.color = Color.red;
            isBerserker = true;

            speed *= 2.5f;
            groundAttackCooldown /= 2;
            groundUpCooldown /= 2;
            punchAttackCooldown /= 2;
            swingAttackCooldown /= 2;
        }

        if ((distance <= startGroundAttackMaxDistance && canGroundAttack && !isAttacking) || (canGroundUp && !isAttacking))
        {
            animator.SetBool("walk", false);

            animator.SetTrigger("attack1");
        }

        else if (distance <= startPunchAttackMaxDistance && canPunchAttack && !isAttacking)
        {
            animator.SetBool("walk", false);

            animator.SetTrigger("attack2");
        }

        else if (distance <= startSwingAttackMaxDistance && canSwingAttack && !isAttacking)
        {
            animator.SetBool("walk", false);

            animator.SetTrigger("attack3");
        }

        else if (distance >= startRunMinDistance)
        {
            animator.SetBool("walk", true);
        }
    }


    // Move
    public void Walk()
    {
        ChooseAction();
        Move();
        Flip();
    }

    public void WalkExit()
    {
        rb.velocity = Vector2.zero;
    }

    private void Move()
    {
        Vector3 direction = player.transform.position - transform.position;
        if (direction.magnitude <= stopRunMinDistance)
        {
            animator.SetBool("walk", false);
            return;
        }

        direction = direction.normalized;
        rb.velocity = direction * speed;
    }

    private void Flip()
    {
        Vector3 direction = player.transform.position - transform.position;
        if (isFacingRight && direction.x < 0 || !isFacingRight && direction.x > 0)
        {
            isFacingRight = !isFacingRight;

            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }

    // Death
    public void Death()
    {
    }

//Skill
    //Ground Attack
    public void GroundAttack()
    {
        canGroundAttack = false;
        isAttacking = true;
    }

    public void GroundAttackSwingStart()
    {
        if (canGroundUp)
        {
            canGroundUp = false;
            StartCoroutine(GroundUp());
        }
    }

    public void GroundAttackSwing()
    {
        attack.Attack(3, groundAttackRange);
    }

    public void GroundAttackExit()
    {
        isAttacking = false;

        StartCoroutine(GroundAttackCooldown());
    }

    IEnumerator GroundUp()
    {
        for(int i = 0; i < 8; i++)
        {
            groundGenerator.Circle(12);
            yield return new WaitForSeconds(0.3f);
        }
        StartCoroutine(GroundUpCooldown());
    }

    IEnumerator GroundUpCooldown()
    {
        yield return new WaitForSeconds(groundUpCooldown);
        canGroundUp = true;
    }

    IEnumerator GroundAttackCooldown()
    {
        yield return new WaitForSeconds(groundAttackCooldown);
        canGroundAttack = true;
    }

    // punch attack
    public void Punch()
    {
        canPunchAttack = false;
        isAttacking = true;
    }

    public void PunchSwing()
    {
        attack.Attack(2, punchAttackRange);
        attack.Stun(punchAttackRange, 1f);
    }

    public void PunchExit()
    {
        isAttacking = false;
        StartCoroutine(PunchCooldown());
    }

    IEnumerator PunchCooldown()
    {
        yield return new WaitForSeconds(punchAttackCooldown);
        canPunchAttack = true;
    }

    // swing attack
    public void SwingAttackStart()
    {
        canSwingAttack = false;
        isAttacking = true;
    }

    public void SwingAttackUpdate()
    {
        if (canGroundUp)
        {
            canGroundUp = false;
            StartCoroutine(GroundUp());
        }
        attack.Attack(3, swingAttackRange);
        attack.Stun(swingAttackRange, 1.5f);
    }

    public void SwingAttackExit()
    {
        isAttacking = false;
        StartCoroutine(SwingAttackCooldown());
    }

    IEnumerator SwingAttackCooldown()
    {
        yield return new WaitForSeconds(swingAttackCooldown);
        canSwingAttack = true;
    }
}
