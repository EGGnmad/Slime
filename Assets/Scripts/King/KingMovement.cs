using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingMovement : StateModel
{
    [Header("Move")]
    [SerializeField] private float speed = 3f;
    Vector2 moveVelocity;

    [Header("Action Settings")]
    [SerializeField] private float startFollowMinDistance = 0.2f;
    [SerializeField] private float stopFollowMinDistance = 0.2f;

    [Header("Game")]
    private GameObject player;

    [Header("Others")]
    private bool isFacingRight = true;

    private Rigidbody2D rb;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        player = GameObject.Find("Player");
    }

    public override void StateEnter(string stateName)
    {
    }

    public override void StateUpdate(string stateName)
    {
        switch (stateName)
        {
            case "Idle":
                ChooseAction();
                break;
            case "Run":
                RunUpdate();
                break;
        }
    }

    public override void StateExit(string stateName)
    {
        switch (stateName)
        {
            case "Run":
                RunExit();
                break;
        }
    }

    // Idle
    private void ChooseAction()
    {
        Vector3 direction = player.transform.position - transform.position;

        //TODO: Add actions (dash, attack)

        if(direction.magnitude >= startFollowMinDistance)
        {
            animator.SetBool("run", true);
        }
    }


    // Move
    private void RunUpdate()
    {
        Move();
        Flip();
    }
    private void RunExit()
    {
        rb.velocity = Vector2.zero;
    }

    private void Move()
    {
        Vector3 direction = player.transform.position - transform.position;
        if (direction.magnitude <= stopFollowMinDistance)
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
}
