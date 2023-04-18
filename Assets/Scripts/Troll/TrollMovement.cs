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

    [Header("Skill")]
    [SerializeField] private float groundAttackCooldown = 1f;
    [SerializeField] private float groundUpCooldown = 10f;
    [SerializeField] private bool canGroundAttack = true;
    [SerializeField] private bool canGroundUp = true;
    
    private bool isAttacking = false;


    [Header("Skill Settings")]
    [SerializeField] private float startGroundAttackMaxDistance = 2f;
    [SerializeField] private float groundAttackRange = 3f;



    [Header("Others")]


    private GameObject player;
    private bool isFacingRight = true;

    private Rigidbody2D rb;
    private TrollModel model;
    private Animator animator;
    private AttackClass attack;
    private GroundUpGenerator groundGenerator;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        model = GetComponent<TrollModel>();
        attack = GetComponent<AttackClass>();
        groundGenerator = GetComponent<GroundUpGenerator>();

        player = GameObject.Find("Player");
    }

    // Idle
    public void ChooseAction()
    {
        Vector3 direction = player.transform.position - transform.position;
        float distance = direction.magnitude;

        if((distance <= startGroundAttackMaxDistance && canGroundAttack && !isAttacking) || (canGroundUp && !isAttacking))
        {
            animator.SetBool("walk", false);

            animator.SetTrigger("attack1");
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
}
