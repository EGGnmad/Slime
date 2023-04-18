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
    private bool isAttacking = false;


    [Header("Skill Settings")]


    [Header("Others")]


    private GameObject player;
    private bool isFacingRight = true;

    private Rigidbody2D rb;
    private Animator animator;
    private AttackClass attack;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        //model = GetComponent<KingModel>();
        attack = GetComponent<AttackClass>();

        player = GameObject.Find("Player");
    }

    // Idle
    public void ChooseAction()
    {
        Vector3 direction = player.transform.position - transform.position;
        float distance = direction.magnitude;

        if (distance >= startRunMinDistance)
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
    //Attack1

}
